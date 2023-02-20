using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryPersonalInfoGet(string Login) : IRequest<ResponsePersonalInfo>;