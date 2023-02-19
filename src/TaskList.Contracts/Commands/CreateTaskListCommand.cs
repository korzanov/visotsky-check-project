using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CreateTaskListCommand() : IRequest<TaskListResponse>;