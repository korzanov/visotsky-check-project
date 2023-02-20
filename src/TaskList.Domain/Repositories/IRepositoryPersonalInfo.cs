using TaskList.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskList.Domain.Repositories;

public interface IRepositoryPersonalInfo
{
    Task<IPersonalInfo> GetPersonalInfo(string login);
    Task<IPersonalInfo> UpdatePersonalInfo(IPersonalInfo newPersonalInfo);
    Task DeletePersonalInfo(string login);
    Task CreatePersonalInfo(string login, string password);
}