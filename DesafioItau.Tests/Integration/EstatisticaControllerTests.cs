using System.Net;
using System.Net.Http.Json;
using DesafioItau.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;


namespace DesafioItau.Tests.Integration;


public class EstatisticaControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task FluxoCompleto_PostGetDelete_AtualizaEstatistica()
    {
        //Arrange
        var client = factory.CreateClient();
        var agora = DateTimeOffset.UtcNow;

        //Act
        await client.PostAsJsonAsync("/transacao", new
        {
            valor = 100, dataHora = agora
        });
        await client.PostAsJsonAsync("/transacao", new
        {
            valor= 50, dataHora = agora
        });

        var antes = await
        client.GetFromJsonAsync<Estatistica>("/estatistica");
        Assert.Equal(2, antes!.Count);
        Assert.Equal(150, antes.Sum);
        Assert.Equal(75, antes.Avg);

        //APAGA TUDO E CONFERE

        var respostaDelete = await client.DeleteAsync("/transacao");
        var depois = await
        client.GetFromJsonAsync<Estatistica>("/estatistica");
        Assert.Equal(0, depois!.Count);

    }

}