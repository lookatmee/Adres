using Microsoft.AspNetCore.Mvc;
using Adres.Application.DTOs;
using Adres.Application.Services;

namespace Adres.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentacionController : ControllerBase
{
    private readonly IDocumentacionService _service;
    private readonly ILogger<DocumentacionController> _logger;

    public DocumentacionController(IDocumentacionService service, ILogger<DocumentacionController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los documentos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DocumentacionDto>>> GetAll()
    {
        try
        {
            var documentos = await _service.GetAllAsync();
            return Ok(documentos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los documentos");
            return BadRequest(new { message = "Error al obtener los documentos", error = ex.Message });
        }
    }

    /// <summary>
    /// Crea un nuevo documento
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DocumentacionDto>> Create([FromBody] CreateDocumentacionDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateAsync(createDto);
            
            _logger.LogInformation("Documento creado exitosamente: {DocumentoId}", result.Id);
            
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear documento");
            return BadRequest(new { message = "Error al crear el documento", error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene un documento por su ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DocumentacionDto>> GetById(int id)
    {
        var documento = await _service.GetByIdAsync(id);
        if (documento == null)
        {
            return NotFound();
        }
        return Ok(documento);
    }

    /// <summary>
    /// Obtiene todos los documentos de una adquisición
    /// </summary>
    [HttpGet("adquisicion/{adquisicionId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DocumentacionDto>>> GetByAdquisicionId(int adquisicionId)
    {
        var documentos = await _service.GetByAdquisicionIdAsync(adquisicionId);
        return Ok(documentos);
    }

    /// <summary>
    /// Elimina un documento por su ID
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            _logger.LogInformation("Documento eliminado exitosamente: {DocumentoId}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar documento {DocumentoId}", id);
            return BadRequest(new { message = "Error al eliminar el documento", error = ex.Message });
        }
    }
} 