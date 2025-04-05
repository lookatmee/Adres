using Microsoft.AspNetCore.Mvc;
using Adres.Application.DTOs;
using Adres.Application.Services;

namespace Adres.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoBienServicioController : ControllerBase
{
    private readonly ITipoBienServicioService _service;

    public TipoBienServicioController(ITipoBienServicioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<TipoBienServicioDto>> Create(CreateTipoBienServicioDto createDto)
    {
        var result = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TipoBienServicioDto>> GetById(int id)
    {
        var tipo = await _service.GetByIdAsync(id);
        if (tipo == null) return NotFound();
        return Ok(tipo);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoBienServicioDto>>> GetAll()
    {
        var tipos = await _service.GetAllAsync();
        return Ok(tipos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTipoBienServicioDto updateDto)
    {
        await _service.UpdateAsync(id, updateDto);
        return NoContent();
    }
} 