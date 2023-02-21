using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TaskList.WebApi.Tests.Integration;

public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AuthTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
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
}