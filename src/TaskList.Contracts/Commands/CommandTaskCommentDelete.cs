using MediatR;

namespace TaskList.Contracts.Commands;

public record CommandTaskCommentDelete(Guid Id) : IRequest;