using Microsoft.AspNetCore.Mvc;
using Adres.Application.DTOs;
using Adres.Application.Services;

namespace Adres.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedorController : ControllerBase
{
    private readonly IProveedorService _service;

    public ProveedorController(IProveedorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ProveedorDto>> Create(CreateProveedorDto createDto)
    {
        var result = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProveedorDto>> GetById(int id)
    {
        var proveedor = await _service.GetByIdAsync(id);
        if (proveedor == null) return NotFound();
        return Ok(proveedor);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetAll()
    {
        var proveedores = await _service.GetAllAsync();
        return Ok(proveedores);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProveedorDto updateDto)
    {
        await _service.UpdateAsync(id, updateDto);
        return NoContent();
    }
} 