using AutoMapper;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;

namespace TaskList.Services.MapProfiles;

public class MapProfileTaskStatusRecord : Profile
{
    public MapProfileTaskStatusRecord()
    {
        CreateMap<CommandTaskStatusRecordCreate, Domain.Entities.TaskStatusRecord>();
        CreateMap<Domain.Entities.TaskStatusRecord, ResponseTaskStatusRecord>();
    }
}