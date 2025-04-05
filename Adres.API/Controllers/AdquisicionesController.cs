using Microsoft.AspNetCore.Mvc;
using Adres.Application.DTOs;
using Adres.Application.Services;

namespace Adres.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdquisicionesController : ControllerBase
{
    private readonly IAdquisicionService _adquisicionService;

    public AdquisicionesController(IAdquisicionService adquisicionService)
    {
        _adquisicionService = adquisicionService;
    }

    [HttpPost]
    public async Task<ActionResult<AdquisicionDto>> Create(CreateAdquisicionDto createDto)
    {
        var result = await _adquisicionService.CreateAsync(createDto, User.Identity?.Name ?? "sistema");
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdquisicionDto>> GetById(int id)
    {
        var adquisicion = await _adquisicionService.GetByIdAsync(id);
        if (adquisicion == null) return NotFound();
        return Ok(adquisicion);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdquisicionDto>>> GetAll()
    {
        var adquisiciones = await _adquisicionService.GetAllAsync();
        return Ok(adquisiciones);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateAdquisicionDto updateDto)
    {
        await _adquisicionService.UpdateAsync(id, updateDto, User.Identity?.Name ?? "sistema");
        return NoContent();
    }

    [HttpPatch("{id}/desactivar")]
    public async Task<IActionResult> Desactivar(int id)
    {
        await _adquisicionService.DesactivarAsync(id, User.Identity?.Name ?? "sistema");
        return NoContent();
    }
} 