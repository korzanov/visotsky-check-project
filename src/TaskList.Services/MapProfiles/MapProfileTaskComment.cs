using AutoMapper;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;

namespace TaskList.Services.MapProfiles;

public class MapProfileTaskComment : Profile
{
    public MapProfileTaskComment()
    {
        CreateMap<CommandTaskCommentCreate, Domain.Entities.TaskComment>();
        CreateMap<CommandTaskCommentUpdate, Domain.Entities.TaskComment>();
        CreateMap<Domain.Entities.TaskComment, ResponseTaskComment>();
    }
}