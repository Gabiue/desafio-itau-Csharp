using DesafioItau.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioItau.Api.Controllers;

[ApiController]
[Route("estatistica")]

public class EstatisticaController(ITransacaoService service): ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(service.ObterEstatisticas());
    }
}