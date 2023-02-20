using Microsoft.AspNetCore.Identity;

namespace TaskList.WebApi.StartUps;

public static class StartUpIdentity
{
    public static void UseEasyPassword(IdentityOptions options)
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 1;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
        options.User.RequireUniqueEmail = false;
    }
}