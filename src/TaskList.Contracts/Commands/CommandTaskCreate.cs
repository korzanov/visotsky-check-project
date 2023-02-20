using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskCreate(string Name, string Description, Guid TaskListId) : IRequest<ResponseTask>;