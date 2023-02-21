using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
using static TaskList.WebApi.Controllers.RouteConstants;

namespace TaskList.WebApi.Tests.Helpers;

public static class HelperAuth
{
    public static void SetJwtToken(HttpClient client, string? jwtToken)
    {
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
    }

    public static async Task<HttpClient> GetAuthoredClient(WebApplicationFactory<Program> factory)
    {
        var login = Guid.NewGuid().ToString();
        var password = "admin";
        var client = factory.CreateClient();
        var jsonContent = JsonContent.Create(new CommandPersonalInfoCreate(login, password));
        await client.PostAsync(UriUsers, jsonContent);
        jsonContent = JsonContent.Create(new QueryAuth(login, password));
        var authResponse = await client.PostAsync(UriSecurityCreateToken, jsonContent);
        string? jwt;
        using (var s = new StreamReader(await authResponse.Content.ReadAsStreamAsync()))
            jwt = await s.ReadLineAsync();
        SetJwtToken(client, jwt);
        return client;
    }
}