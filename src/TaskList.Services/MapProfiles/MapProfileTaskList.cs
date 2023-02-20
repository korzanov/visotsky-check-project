using AutoMapper;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;

namespace TaskList.Services.MapProfiles;

public class MapProfileTaskList : Profile
{
    public MapProfileTaskList()
    {
        CreateMap<CommandTaskListCreate, Domain.Entities.TaskList>();
        CreateMap<Domain.Entities.TaskList, ResponseTaskList>();
        CreateMap<CommandTaskListCreate, Domain.Entities.TaskList>();
        CreateMap<CommandTaskListUpdate, Domain.Entities.TaskList>();
    }
}