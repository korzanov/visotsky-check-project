using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskStatusRecordCreate(Guid TaskId, Guid TaskStatusId) : IRequest<ResponseTaskStatusRecord>;