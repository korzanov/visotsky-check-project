using TaskList.Contracts;
using TaskList.Domain.Repositories;
using TaskList.Services.Abstractions;

namespace TaskList.Services;

internal sealed class AuthService : ServiceBase, IAuthService
{
    public AuthService(IRepositoryManager repositoryManager) : base(repositoryManager)
    { }

    public Task<bool> AuthAsync(UserAuthDto userAuthDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}