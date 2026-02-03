using ComprasVentas.Dto;
using ComprasVentas.Services.specification;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoService _permisoService;

    public PermisoController(IPermisoService permisoService)
    {
        _permisoService = permisoService;
    }

    // GET: api/permiso
    [HttpGet]
    public async Task<ActionResult<List<PermisoDto>>> GetAll()
    {
        var permisos = await _permisoService.GetAllPermisosAsync();
        return Ok(permisos);
    }

    // GET: api/permiso/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PermisoDto>> GetById(int id)
    {
        var permiso = await _permisoService.GetPermisoByIdAsync(id);

        if (permiso is null)
            return NotFound(new { message = $"Permiso con id {id} no encontrado" });

        return Ok(permiso);
    }

    // POST: api/permiso
    [HttpPost]
    public async Task<ActionResult<PermisoDto>> Create([FromBody] CreatePermisoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var permisoCreado = await _permisoService.CreatePermisoAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = permisoCreado.Id },
            permisoCreado
        );
    }
    }
}
