using TaskList.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskList.Domain.Interfaces;

public interface IPersonalInfoRepository
{
    Task<IPersonalInfo> GetPersonalInfo(string userName);
    Task<IPersonalInfo> UpdatePersonalInfo(IPersonalInfo newPersonalInfo);
    Task DeletePersonalIno(string userName);
}