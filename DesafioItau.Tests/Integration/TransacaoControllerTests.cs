using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;


namespace DesafioItau.Tests.Integration;

public class TransacaoControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private static StringContent Json(string json)=>
    new(json, Encoding.UTF8, "application/json");

    [Fact] 
    public async Task Post_TransacaoValida_Retorna201()
    {
        //Arrange
        var client = factory.CreateClient();
        var corpo = Json("""{"Valor": 100.50, "dataHora":"2020-01-01T12:00:00Z"}""");
        //Act
        var resposta = await client.PostAsync("/transacao", corpo);

    //Assert 
    Assert.Equal(HttpStatusCode.Created,  resposta.StatusCode);
    }

    [Theory]
    [InlineData("""{ "valor": -1, "dataHora": "2020-01-01T12:00:00Z" }""")]
    [InlineData("""{ "valor": 100, "dataHora": "2999-01-01T12:00:00Z" }""")]
    [InlineData("""{ "dataHora": "2020-01-01T12:00:00Z" }""")]
    [InlineData("""{ "valor": 100 }""")]
    [InlineData("""{}""")]

     public async Task Post_DadosInvalidos_Retorna422(string json)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var resposta = await client.PostAsync("/transacao", Json(json));

        // Assert
        Assert.Equal(HttpStatusCode.UnprocessableEntity, resposta.StatusCode);
    }

    [Theory]
    [InlineData("""{ "valor": 100""")]
    [InlineData("""isso nao e json""")]
    [InlineData("""{ "valor": "abc", "dataHora": "2020-01-01T12:00:00Z" }""")]
    [InlineData("""{ "valor": 100, "dataHora": "ontem" }""")]
    public async Task Post_JsonMalFormado_Retorna400(string json)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var resposta = await client.PostAsync("/transacao", Json(json));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
    }
}