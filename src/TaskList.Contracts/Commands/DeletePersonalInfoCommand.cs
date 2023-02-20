using MediatR;

namespace TaskList.Contracts.Commands;

public record DeletePersonalInfoCommand(string Login) : IRequest;