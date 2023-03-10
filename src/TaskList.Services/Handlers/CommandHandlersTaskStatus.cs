using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class CommandHandlersTaskStatus : IRequestHandler<CommandTaskStatusSetDefaults>
{
    private readonly IRepository<Domain.Entities.TaskStatus> _repository;
    private readonly IMapper _mapper;
    private static readonly object Sync = new();

    public CommandHandlersTaskStatus(IRepository<Domain.Entities.TaskStatus> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task Handle(CommandTaskStatusSetDefaults request, CancellationToken cancellationToken)
    {
        foreach (var status in ResponseTaskStatus.Defaults)
        {
            lock (Sync)
            {
                var existStatus = _repository.GetByIdAsync(status.Id, cancellationToken).Result;
                if (existStatus is not null)
                    continue;
                var newStatus = _mapper.Map<Domain.Entities.TaskStatus>(status);
                _repository.AddAsync(newStatus, cancellationToken).GetAwaiter();
                _repository.SaveChangesAsync(cancellationToken).GetAwaiter();
            }
        }

        return Task.CompletedTask;
    }
}