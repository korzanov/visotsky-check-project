using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskStatusRecordGetLast(Guid TaskId) : IRequest<ResponseTaskStatusRecord>;