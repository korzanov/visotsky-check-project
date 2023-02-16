namespace TaskList.Services.Abstractions;

public interface ICrudService<TDto, in TForCreationDto, in TForUpdateDto>
{
    Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TDto> GetByIdAsync(Guid entityId, CancellationToken cancellationToken = default);
    Task<TDto> CreateAsync(TForCreationDto createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid entityId, TForUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default);
}