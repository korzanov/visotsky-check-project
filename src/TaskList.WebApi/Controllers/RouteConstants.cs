// ReSharper disable InconsistentNaming
namespace TaskList.WebApi.Controllers;

public static class RouteConstants
{
    public const string UriUsers = "/api/users";
    public const string UriTaskLists = "/api/taskLists";
    public const string UriTasks = "/api/tasks";
    public const string UriTasks_StatusChange = "status/change";
    public const string UriTasks_ListChange = "list/change";
    public const string UriTasks_List = "list";
    public const string UriTaskComments = "/api/taskComments";

    public const string UriSecurityCreateToken = "/security/createToken";
    public const string UriSecurityCheckToken = "/security/checkToken";
}