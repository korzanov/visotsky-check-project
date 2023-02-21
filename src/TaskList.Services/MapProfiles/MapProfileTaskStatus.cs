using AutoMapper;
using TaskList.Contracts.Responses;

namespace TaskList.Services.MapProfiles;

public class MapProfileTaskStatus : Profile
{
    public MapProfileTaskStatus()
    {
        CreateMap<ResponseTaskStatus, Domain.Entities.TaskStatus>();
        CreateMap<Domain.Entities.TaskStatus, ResponseTaskStatus>();
    }
}