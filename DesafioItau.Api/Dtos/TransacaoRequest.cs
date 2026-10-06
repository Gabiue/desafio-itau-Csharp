namespace DesafioItau.Api.Dtos;

public record TransacaoRequest(decimal? Valor, DateTimeOffset? DataHora);