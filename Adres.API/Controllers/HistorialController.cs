using Microsoft.AspNetCore.Mvc;
using Adres.Application.DTOs;
using Adres.Application.Services;

namespace Adres.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistorialController : ControllerBase
{
    private readonly IHistorialService _historialService;

    public HistorialController(IHistorialService historialService)
    {
        _historialService = historialService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HistorialCambioDto>>> GetAll()
    {
        var historial = await _historialService.GetAllAsync();
        return Ok(historial);
    }

    [HttpGet("adquisicion/{id}")]
    public async Task<ActionResult<IEnumerable<HistorialCambioDto>>> GetByAdquisicionId(int id)
    {
        var historial = await _historialService.GetByAdquisicionIdAsync(id);
        return Ok(historial);
    }
} 