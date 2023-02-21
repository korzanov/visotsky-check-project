using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskCommentCreate(Guid TaskId, string Message) : IRequest<ResponseTaskComment>;