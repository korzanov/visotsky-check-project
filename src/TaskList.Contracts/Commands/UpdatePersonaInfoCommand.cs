using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record UpdatePersonalInfoCommand(string Login, string Name, string Email) : IRequest<PersonalInfoResponse>;