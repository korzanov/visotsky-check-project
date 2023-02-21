using MediatR;

namespace TaskList.Contracts.Commands;

public record CommandTaskStatusSetDefaults() : IRequest;