using TaskList.Domain.Entities;

namespace TaskList.Services;

internal record struct PersonalInfoMiddleObject(string Login, string Name, string Email) : IPersonalInfo;
