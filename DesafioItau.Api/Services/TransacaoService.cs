using DesafioItau.Api.Models;

namespace DesafioItau.Api.Services;

public class TransacaoService : ITransacaoService
{
    private readonly List<Transacao> _transacoes = [];
    private readonly Lock _lock = new();

    public void Adicionar(Transacao transacao)
    {
        lock (_lock)
        {
            _transacoes.Add(transacao);
        }
    }
    public void Limpar()
    {
        lock (_lock)
        {
            _transacoes.Clear();
        }
    }

    public Estatistica ObterEstatisticas()
    {
        var limite = DateTimeOffset.UtcNow.AddSeconds(-60);

        lock (_lock)
        {
            var ultimas = _transacoes.Where(t=>t.DataHora>=limite).ToList();

            if(ultimas.Count == 0)
            {
                return new Estatistica(0,0,0,0,0);
            }
            return new Estatistica(
                ultimas.Count,
                ultimas.Sum(t=>t.Valor),
                ultimas.Average(t=>t.Valor),
                ultimas.Min(t=>t.Valor),
                ultimas.Max(t=>t.Valor)
            );
        }
    }
}