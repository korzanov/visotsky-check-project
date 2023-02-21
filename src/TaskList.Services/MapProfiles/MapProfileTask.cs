using AutoMapper;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;

namespace TaskList.Services.MapProfiles;

public class MapProfileTask : Profile
{
    public MapProfileTask()
    {
        CreateMap<CommandTaskCreate, Domain.Entities.Task>();
        CreateMap<Domain.Entities.Task, ResponseTask>();
        CreateMap<CommandTaskUpdate, Domain.Entities.Task>();
    }
}