namespace TaskList.Contracts.Responses;

public record ResponseTaskStatusRecord(Guid Id, DateTime DateTime, string? StatusName = null);