using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskListGet(Guid Id) : IRequest<ResponseTaskList>;