using DesafioItau.Api.Models;

namespace DesafioItau.Api.Services;

public interface ITransacaoService
{
    void Adicionar(Transacao transacao);
    void Limpar();

    Estatistica ObterEstatisticas();
}