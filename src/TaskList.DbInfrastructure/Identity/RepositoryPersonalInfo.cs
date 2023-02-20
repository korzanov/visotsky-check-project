using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using TaskList.Domain.Entities;
using TaskList.Domain.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskList.DbInfrastructure.Identity;

public class RepositoryPersonalInfo : IRepositoryPersonalInfo
{
    private readonly UserManager<TaskListAppUser> _manager;

    public RepositoryPersonalInfo(UserManager<TaskListAppUser> manager) => 
        _manager = manager;

    public async Task<IPersonalInfo> GetPersonalInfo(string login)
    {
        return await _manager.FindByNameAsync(login) 
               ?? throw new NotFoundException(login, nameof(IPersonalInfo));
    }

    public async Task<IPersonalInfo> UpdatePersonalInfo(IPersonalInfo newPersonalInfo)
    {
        var personalInfoForUpdate = await _manager.FindByNameAsync(newPersonalInfo.Login);
        Guard.Against.NotFound(newPersonalInfo.Login, personalInfoForUpdate, nameof(personalInfoForUpdate));
        personalInfoForUpdate.Name = newPersonalInfo.Name;
        personalInfoForUpdate.Email = newPersonalInfo.Email;
        var result = await _manager.UpdateAsync(personalInfoForUpdate);
        if (result.Succeeded)
            return personalInfoForUpdate;
        throw new Exception(JsonSerializer.Serialize(result.Errors));
    }

    public async Task DeletePersonalInfo(string login)
    {        
        var personalInfo = await _manager.FindByNameAsync(login);
        Guard.Against.NotFound(login, personalInfo, nameof(personalInfo));
        var result = await _manager.DeleteAsync(personalInfo);
        if (result.Succeeded)
            return;
        throw new Exception(JsonSerializer.Serialize(result.Errors));
    }
}