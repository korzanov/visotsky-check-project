using AutoMapper;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Entities;

namespace TaskList.Services.MapProfiles;

public class MapProfilePersonalInfo : Profile
{
    public MapProfilePersonalInfo()
    {
        CreateMap<IPersonalInfo, ResponsePersonalInfo>();
        CreateMap<CommandPersonalInfoUpdate, PersonalInfoMiddleObject>();
    }
}