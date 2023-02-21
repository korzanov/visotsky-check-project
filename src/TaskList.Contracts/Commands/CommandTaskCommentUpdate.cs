using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskCommentUpdate(Guid Id, string Message) : IRequest<ResponseTaskComment>;