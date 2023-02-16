using TaskList.Contracts;
using TaskList.Domain.Repositories;
using TaskList.Services.Abstractions;

namespace TaskList.Services;

internal sealed class UserService : ServiceBase, IUserService
{
    public UserService(IRepositoryManager repositoryManager) : base(repositoryManager)
    { }

    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> GetByIdAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> CreateAsync(UserCreateDto createDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid entityId, UserUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}