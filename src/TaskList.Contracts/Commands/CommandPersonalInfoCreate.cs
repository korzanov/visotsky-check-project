using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandPersonalInfoCreate(string Login, string Password) : IRequest<ResponsePersonalInfo>;