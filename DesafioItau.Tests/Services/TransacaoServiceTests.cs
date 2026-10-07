using System.Runtime.CompilerServices;
using DesafioItau.Api.Config;
using DesafioItau.Api.Models;
using DesafioItau.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DesafioItau.Tests.Services;


public class TransacaoServiceTests
{
    private static TransacaoService CriarService()
    {
        var options = Options.Create(new EstatisticaOptions{
            JanelaEmSegundos = 60});
            return new TransacaoService(options);
    }
    [Fact]
    public void ObterEstatisticas_SemTransacoes_RetornaTudoZero()
    {
        //Arrange 
        var service = CriarService();
        //Act
        var resultado = service.ObterEstatisticas();
        //Assert
        Assert.Equal(0, resultado.Count);
        Assert.Equal(0, resultado.Sum);
        Assert.Equal(0, resultado.Avg);
        Assert.Equal(0, resultado.Min);
        Assert.Equal(0, resultado.Max);

    }

    [Fact]
    public void ObterEstatisticas_ComTransacoesRecentes_CalculaCorretamente()
    {
        //Arrange
        var service = CriarService();
        service.Adicionar(new Transacao(100, DateTimeOffset.UtcNow));
        service.Adicionar(new Transacao(50, DateTimeOffset.UtcNow));
        //act
        var resultado = service.ObterEstatisticas();
        //Assert
        Assert.Equal(2, resultado.Count);
        Assert.Equal(150, resultado.Sum);
        Assert.Equal(75, resultado.Avg);
        Assert.Equal(50, resultado.Min);
        Assert.Equal(100, resultado.Max);
    }

    [Fact]
    public void ObterEstatisticas_ComTransacaoAntiga_IgnoraTransacao()
    {
        //Arrange
        var service = CriarService();
        service.Adicionar(new Transacao(999, DateTimeOffset.UtcNow.AddSeconds(-120)));
        service.Adicionar(new Transacao(150, DateTimeOffset.UtcNow));
        service.Adicionar(new Transacao(100,DateTimeOffset.UtcNow));
        service.Adicionar(new Transacao(50, DateTimeOffset.UtcNow));
        //act
        var resultado = service.ObterEstatisticas();
        //assert
        Assert.Equal(3, resultado.Count);
        Assert.Equal(300, resultado.Sum);
        Assert.Equal(100, resultado.Avg);
        Assert.Equal(50, resultado.Min);
        Assert.Equal(150, resultado.Max);

    }

    [Fact]
    public void Limpar_ComTransacoes_RemoveTodas()
    {
        //arrange
        var service = CriarService();

        service.Adicionar(new Transacao(999, DateTimeOffset.UtcNow.AddSeconds(-120)));
        service.Adicionar(new Transacao(150, DateTimeOffset.UtcNow));
        service.Adicionar(new Transacao(100,DateTimeOffset.UtcNow));
        service.Adicionar(new Transacao(50, DateTimeOffset.UtcNow));
        //Act
        service.Limpar();
        var resultado = service.ObterEstatisticas();


        //assert
        Assert.Equal(0, resultado.Count);
        Assert.Equal(0, resultado.Sum);
        Assert.Equal(0, resultado.Avg);
        Assert.Equal(0, resultado.Min);
        Assert.Equal(0, resultado.Max);
    }


}