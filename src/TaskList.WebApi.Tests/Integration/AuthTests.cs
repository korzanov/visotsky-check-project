using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.WebApi.Tests.Integration;

public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AuthTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Theory]
    [InlineData("login", "password")]
    [InlineData("admin", "admin")]
    public async Task CreateUser(string login, string password)
    {
        var client = _factory.CreateClient();
        
        var jsonContent = JsonContent.Create(new CommandPersonalInfoCreate(login, password));
        var response = await client.PostAsync("/api/users", jsonContent);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        jsonContent = JsonContent.Create(new QueryAuth(login, password));
        var authResponse = await client.PostAsync("/security/createToken", jsonContent);
        Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);

        string? jwt;
        using (var s = new StreamReader(await authResponse.Content.ReadAsStreamAsync()))
            jwt = await s.ReadLineAsync();
        
        var check1 = await client.GetAsync("/security/checkToken");
        Assert.Equal(HttpStatusCode.Unauthorized, check1.StatusCode);
        
        Helpers.HelperAuth.SetJwtToken(client, jwt);

        var check2 = await client.GetAsync("/security/checkToken");
        Assert.Equal(HttpStatusCode.OK, check2.StatusCode);

    }

    [Theory]
    [InlineData("/security/checkToken")]
    [InlineData("/api/tasks")]
    [InlineData("/api/tasks/list/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/tasks/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/taskLists")]
    [InlineData("/api/taskLists/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/taskComments/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/users/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    public async Task Get_EndpointsReturnUnauthorized(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Theory]
    [InlineData("/security/checkToken")]
    [InlineData("/api/tasks")]
    [InlineData("/api/tasks/list/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/tasks/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/taskLists")]
    [InlineData("/api/taskLists/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/taskComments/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/users/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    public async Task Get_EndpointsAuthorized(string url)
    {
        // Arrange
        var client = await Helpers.HelperAuth.GetAuthoredClient(_factory);

        // Act
        var response = await client.GetAsync(url);

        // Assert
        Assert.NotEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Theory]
    [InlineData("/api/tasks")]
    [InlineData("/api/taskLists")]
    [InlineData("/api/taskComments")]
    public async Task Post_EndpointsReturnUnauthorized(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsync(url, null);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Theory]
    [InlineData("/api/tasks")]
    [InlineData("/api/taskLists")]
    [InlineData("/api/taskComments")]
    public async Task Post_EndpointsAuthorized(string url)
    {
        // Arrange
        var client = await Helpers.HelperAuth.GetAuthoredClient(_factory);

        // Act
        var response = await client.PostAsync(url, null);

        // Assert
        Assert.NotEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Theory]
    [InlineData("/api/tasks")]
    [InlineData("/api/tasks/changeList")]
    [InlineData("/api/tasks/changeStatus")]
    [InlineData("/api/taskLists")]
    [InlineData("/api/taskComments")]
    [InlineData("/api/users")]
    public async Task Put_EndpointsReturnUnauthorized(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PutAsync(url, null);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Theory]
    [InlineData("/api/tasks")]
    [InlineData("/api/tasks/changeList")]
    [InlineData("/api/tasks/changeStatus")]
    [InlineData("/api/taskLists")]
    [InlineData("/api/taskComments")]
    [InlineData("/api/users")]
    public async Task Put_EndpointsAuthorized(string url)
    {
        // Arrange
        var client = await Helpers.HelperAuth.GetAuthoredClient(_factory);

        // Act
        var response = await client.PutAsync(url, null);

        // Assert
        Assert.NotEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Theory]
    [InlineData("/api/tasks")]
    [InlineData("/api/taskComments")]
    [InlineData("/api/users")]
    [InlineData("/api/taskLists/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/taskLists/85C46C1F-7FAC-45C6-B761-C17346419D64/to/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    public async Task Delete_EndpointsReturnUnauthorized(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Theory]
    [InlineData("/api/tasks")]
    [InlineData("/api/taskComments")]
    [InlineData("/api/users")]
    [InlineData("/api/taskLists/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    [InlineData("/api/taskLists/85C46C1F-7FAC-45C6-B761-C17346419D64/to/85C46C1F-7FAC-45C6-B761-C17346419D64")]
    public async Task Delete_EndpointsAuthorized(string url)
    {
        // Arrange
        var client = await Helpers.HelperAuth.GetAuthoredClient(_factory);

        // Act
        var response = await client.DeleteAsync(url);

        // Assert
        Assert.NotEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}