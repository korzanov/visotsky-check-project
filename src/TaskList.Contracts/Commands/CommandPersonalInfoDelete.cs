using MediatR;

namespace TaskList.Contracts.Commands;

public record CommandPersonalInfoDelete(string Login) : IRequest;