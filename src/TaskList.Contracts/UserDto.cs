namespace TaskList.Contracts;

public sealed record UserDto(Guid Id, string Name, string Email);
public sealed record UserCreateDto(string Name, string Email);
public sealed record UserUpdateDto(Guid Id, string Name, string Email);