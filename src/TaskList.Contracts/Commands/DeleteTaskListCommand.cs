using MediatR;

namespace TaskList.Contracts.Commands;

public record DeleteTaskListCommand(Guid Id) : IRequest;