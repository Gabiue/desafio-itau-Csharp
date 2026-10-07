using DesafioItau.Api.Dtos;
using DesafioItau.Api.Models;
using DesafioItau.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioItau.Api.Controllers;


[ApiController]
[Route("transacao")]
public class TransacaoController(ITransacaoService service) : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] TransacaoRequest request)
    {
        if(request.Valor is null || request.DataHora is null)
        return UnprocessableEntity();

        if(request.Valor<0|| request.DataHora> DateTimeOffset.UtcNow)
        return UnprocessableEntity();
        var transacao = new Transacao(request.Valor.Value, request.DataHora.Value);
        service.Adicionar(transacao);

        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpDelete]
    public IActionResult Delete()
    {
        service.Limpar();
        return Ok();
    }
}