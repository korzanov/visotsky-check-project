using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
using static TaskList.WebApi.Controllers.RouteConstants;

namespace TaskList.WebApi.Tests.Integration;

public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private const string AnyGuid = "85C46C1F-7FAC-45C6-B761-C17346419D64";

    public AuthTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Theory]
    [InlineData("Pa$$w0rd")]
    public async Task CreateUser(string password)
    {
        var login = Guid.NewGuid().ToString();
        var client = _factory.CreateClient();
        
        var jsonContent = JsonContent.Create(new CommandPersonalInfoCreate(login, password));
        var response = await client.PostAsync(UriUsers, jsonContent);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        jsonContent = JsonContent.Create(new QueryAuth(login, password));
        var authResponse = await client.PostAsync(UriSecurityCreateToken, jsonContent);
        Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);

        string? jwt;
        using (var s = new StreamReader(await authResponse.Content.ReadAsStreamAsync()))
            jwt = await s.ReadLineAsync();
        
        var check1 = await client.GetAsync(UriSecurityCheckToken);
        Assert.Equal(HttpStatusCode.Unauthorized, check1.StatusCode);
        
        Helpers.HelperAuth.SetJwtToken(client, jwt);

        var check2 = await client.GetAsync(UriSecurityCheckToken);
        Assert.Equal(HttpStatusCode.OK, check2.StatusCode);

    }

    [Theory]
    [InlineData(UriSecurityCheckToken)]
    [InlineData(UriTasks)]
    [InlineData($"{UriTasks}/{UriTasks_List}/{AnyGuid}")]
    [InlineData($"{UriTasks}/{AnyGuid}")]
    [InlineData(UriTaskLists)]
    [InlineData($"{UriTaskLists}/{AnyGuid}")]
    [InlineData($"{UriTaskComments}/{AnyGuid}")]
    [InlineData($"{UriUsers}/{AnyGuid}")]
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
    [InlineData(UriSecurityCheckToken)]
    [InlineData(UriTasks)]
    [InlineData($"{UriTasks}/{UriTasks_List}/{AnyGuid}")]
    [InlineData($"{UriTasks}/{AnyGuid}")]
    [InlineData(UriTaskLists)]
    [InlineData($"{UriTaskLists}/{AnyGuid}")]
    [InlineData($"{UriTaskComments}/{AnyGuid}")]
    [InlineData($"{UriUsers}/{AnyGuid}")]
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
    [InlineData(UriTasks)]
    [InlineData(UriTaskLists)]
    [InlineData(UriTaskComments)]
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
    [InlineData(UriTasks)]
    [InlineData(UriTaskLists)]
    [InlineData(UriTaskComments)]
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
    [InlineData(UriTasks)]
    [InlineData($"{UriTasks}/{UriTasks_ListChange}")]
    [InlineData($"{UriTasks}/{UriTasks_StatusChange}")]
    [InlineData(UriTaskLists)]
    [InlineData(UriTaskComments)]
    [InlineData(UriUsers)]
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
    [InlineData(UriTasks)]
    [InlineData($"{UriTasks}/{UriTasks_ListChange}")]
    [InlineData($"{UriTasks}/{UriTasks_StatusChange}")]
    [InlineData(UriTaskLists)]
    [InlineData(UriTaskComments)]
    [InlineData(UriUsers)]
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
    [InlineData(UriTasks)]
    [InlineData(UriTaskComments)]
    [InlineData(UriUsers)]
    [InlineData($"{UriTaskLists}/{AnyGuid}")]
    [InlineData($"{UriTaskLists}/{AnyGuid}/to/{AnyGuid}")]
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
    [InlineData(UriTasks)]
    [InlineData(UriTaskComments)]
    [InlineData(UriUsers)]
    [InlineData($"{UriTaskLists}/{AnyGuid}")]
    [InlineData($"{UriTaskLists}/{AnyGuid}/to/{AnyGuid}")]
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