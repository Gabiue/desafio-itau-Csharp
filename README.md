# desafio-itau-Csharp

API REST em **C# / .NET 10** inspirada no [desafio de backend do Itaú Unibanco](https://github.com/rafaellins-itau/desafio-itau-vaga-99-junior).

> **Aviso:** este projeto **não** é uma entrega oficial nem tem vínculo com o Itaú. O desafio original pede Java ou Kotlin com Spring Boot. Aqui a mesma proposta é reimplementada em C# com ASP.NET Core, para estudo e prática.

## Sobre

A API recebe transações e calcula estatísticas das que aconteceram nos **últimos 60 segundos**. Todos os dados ficam **em memória**, sem banco de dados nem cache.

## Endpoints

| Método   | Rota          | Descrição                                     | Respostas          |
|----------|---------------|-----------------------------------------------|--------------------|
| `POST`   | `/transacao`  | Registra uma transação                        | `201`, `422`, `400` |
| `DELETE` | `/transacao`  | Apaga todas as transações                     | `200`              |
| `GET`    | `/estatistica`| Estatísticas das transações dos últimos 60s   | `200`              |

### Exemplo: `POST /transacao`

```json
{
  "valor": 123.45,
  "dataHora": "2020-08-07T12:34:56.789-03:00"
}
```

Regras de validação:

- `valor` e `dataHora` são obrigatórios
- `valor` deve ser maior ou igual a `0`
- `dataHora` não pode estar no futuro

### Exemplo: `GET /estatistica`

```json
{
  "count": 10,
  "sum": 1234.56,
  "avg": 123.456,
  "min": 12.34,
  "max": 123.56
}
```

Sem transações nos últimos 60 segundos, todos os valores retornam `0`.

## Tecnologias

- C# / .NET 10
- ASP.NET Core
- xUnit (testes)

## Como executar

Pré-requisito: [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
# executar a API
dotnet run --project DesafioItau.Api

# executar os testes
dotnet test
```

## Status

🚧 Em desenvolvimento.
