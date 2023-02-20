using MediatR;

namespace TaskList.Contracts.Commands;

public record CommandTaskDelete(Guid Id) : IRequest;