using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskListCreate(string Name, string Description) : IRequest<TaskListResponse>;