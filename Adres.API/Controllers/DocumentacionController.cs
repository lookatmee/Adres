using Microsoft.AspNetCore.Mvc;
using Adres.Application.DTOs;
using Adres.Application.Services;

namespace Adres.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentacionController : ControllerBase
{
    private readonly IDocumentacionService _service;

    public DocumentacionController(IDocumentacionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<DocumentacionDto>> Create(CreateDocumentacionDto createDto)
    {
        var result = await _service.CreateAsync(createDto);
        return Ok(result);
    }

    [HttpGet("adquisicion/{adquisicionId}")]
    public async Task<ActionResult<IEnumerable<DocumentacionDto>>> GetByAdquisicionId(int adquisicionId)
    {
        var documentos = await _service.GetByAdquisicionIdAsync(adquisicionId);
        return Ok(documentos);
    }
} 