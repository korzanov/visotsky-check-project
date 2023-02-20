using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskListGetPage(int PageNumber) : IRequest<IEnumerable<ResponseTaskList>>;