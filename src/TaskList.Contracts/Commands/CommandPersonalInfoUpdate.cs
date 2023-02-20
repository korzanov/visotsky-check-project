using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandPersonalInfoUpdate(string Login, string Name, string Email) : IRequest<ResponsePersonalInfo>;