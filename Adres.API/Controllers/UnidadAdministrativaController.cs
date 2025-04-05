using Adres.Application.DTOs;
using Adres.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adres.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnidadAdministrativaController : ControllerBase
{
    private readonly IUnidadAdministrativaService _service;

    public UnidadAdministrativaController(IUnidadAdministrativaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<UnidadAdministrativaDto>> Create(CreateUnidadAdministrativaDto createDto)
    {
        var result = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadAdministrativaDto>> GetById(int id)
    {
        var unidad = await _service.GetByIdAsync(id);
        if (unidad == null) return NotFound();
        return Ok(unidad);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnidadAdministrativaDto>>> GetAll()
    {
        var unidades = await _service.GetAllAsync();
        return Ok(unidades);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUnidadAdministrativaDto updateDto)
    {
        await _service.UpdateAsync(id, updateDto);
        return NoContent();
    }
} 