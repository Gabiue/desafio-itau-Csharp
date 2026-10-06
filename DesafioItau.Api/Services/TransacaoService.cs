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
}