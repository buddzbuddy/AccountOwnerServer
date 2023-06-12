using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using Entities.Models;

namespace Integration;

public class IntegrationTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact]
    public async Task GetAllOwners_ReturnsOkResponse()
    {
        //Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/owner");
        response.EnsureSuccessStatusCode();
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Fact]
    public async Task GetAllOwners_ReturnsAListOfOwners()
    {
        //Arrange
        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync("/api/owner");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var owners = JsonConvert.DeserializeObject<List<Owner>>(responseString);
        // Assert
        Assert.NotEmpty(owners);
    }
}