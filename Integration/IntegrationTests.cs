using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Integration;

public class IntegrationTests: IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetAllOwners_ReturnsAListOfOwners()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder()
            .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
            .UseStartup<Startup>());
        var client = server.CreateClient();

        // Act
        var response = await client.GetAsync("/api/owner");
        response.EnsureSuccessStatusCode();
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}