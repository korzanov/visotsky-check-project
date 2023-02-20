using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskListGetAll(Guid Id) : IRequest<ResponseTaskList>;