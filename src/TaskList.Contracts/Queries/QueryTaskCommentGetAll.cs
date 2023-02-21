using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskCommentGetAll(Guid TaskId) : IRequest<IEnumerable<ResponseTaskComment>>;