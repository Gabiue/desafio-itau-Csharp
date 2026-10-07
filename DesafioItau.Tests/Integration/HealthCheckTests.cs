using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;


namespace DesafioItau.Tests.Integration;

public class HealthCheckTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetHealth_ApiNoAr_Retornar200()
    {
        //Arrage 
        var client = factory.CreateClient();
        //act
        var resposta = await client.GetAsync("/health");
        //Assert
        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
    }
}