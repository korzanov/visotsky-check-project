using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record GetPersonalInfoQuery(string Login) : IRequest<PersonalInfoResponse>;