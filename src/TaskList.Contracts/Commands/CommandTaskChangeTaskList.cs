using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskChangeTaskList(Guid Id, Guid TaskListId) : IRequest<ResponseTask>;