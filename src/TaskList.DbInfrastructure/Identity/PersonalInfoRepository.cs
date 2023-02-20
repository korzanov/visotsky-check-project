using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using TaskList.Domain.Entities;
using TaskList.Domain.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskList.DbInfrastructure.Identity;

public class PersonalInfoRepository : IPersonalInfoRepository
{
    private readonly UserManager<TaskListAppUser> _manager;

    public PersonalInfoRepository(UserManager<TaskListAppUser> manager) => 
        _manager = manager;

    public async Task<IPersonalInfo> GetPersonalInfo(string userName)
    {
        return await _manager.FindByNameAsync(userName) 
               ?? throw new NotFoundException(userName, nameof(IPersonalInfo));
    }

    public async Task<IPersonalInfo> UpdatePersonalInfo(IPersonalInfo newPersonalInfo)
    {
        var personalInfoForUpdate = await _manager.FindByNameAsync(newPersonalInfo.UserName);
        Guard.Against.NotFound(newPersonalInfo.UserName, personalInfoForUpdate, nameof(personalInfoForUpdate));
        personalInfoForUpdate.Name = newPersonalInfo.Name;
        personalInfoForUpdate.Email = newPersonalInfo.Email;
        var result = await _manager.UpdateAsync(personalInfoForUpdate);
        if (result.Succeeded)
            return personalInfoForUpdate;
        throw new Exception(JsonSerializer.Serialize(result.Errors));
    }

    public async Task DeletePersonalIno(string userName)
    {        
        var personalInfo = await _manager.FindByNameAsync(userName);
        Guard.Against.NotFound(userName, personalInfo, nameof(personalInfo));
        var result = await _manager.DeleteAsync(personalInfo);
        if (result.Succeeded)
            return;
        throw new Exception(JsonSerializer.Serialize(result.Errors));
    }
}