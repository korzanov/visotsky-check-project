using TaskList.Contracts;

namespace TaskList.Services.Abstractions;

public interface IAuthService
{
    Task<bool> AuthAsync(UserAuthDto userAuthDto, CancellationToken cancellationToken = default);
}