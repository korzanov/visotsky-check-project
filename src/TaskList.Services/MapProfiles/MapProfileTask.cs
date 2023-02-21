using AutoMapper;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;

namespace TaskList.Services.MapProfiles;

public class MapProfileTask : Profile
{
    public MapProfileTask()
    {
        CreateMap<CommandTaskCreate, TaskList.Domain.Entities.Task>();
        CreateMap<TaskList.Domain.Entities.Task, ResponseTask>();
    }
}