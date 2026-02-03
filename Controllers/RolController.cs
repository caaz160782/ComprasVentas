using ComprasVentas.Dto;
using ComprasVentas.Services.specification;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolController : ControllerBase
{
    private readonly IRolService _rolService;

    public RolController(IRolService rolService)
    {
        _rolService = rolService;
    }

    // GET: api/rol
    [HttpGet]
    public async Task<ActionResult<List<RolDto>>> GetAllRoles()
    {
        var roles = await _rolService.GetAllRolesAsync();
        return Ok(roles);
    }

    // GET: api/rol/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RolDto>> GetRolById(int id)
    {
        var rol = await _rolService.GetRolDtoAsync(id);

        if (rol is null)
            return NotFound(new { message = $"Rol con id {id} no encontrado" });

        return Ok(rol);
    }

    // POST: api/rol
    [HttpPost]
    public async Task<ActionResult<RolDto>> CreateRol([FromBody] CreateRolDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var rol = await _rolService.CreateRolAsync(dto);

        return CreatedAtAction(
            nameof(GetRolById),
            new { id = rol.Id },
            rol
        );
    }

    // PUT: api/rol/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRol(int id, [FromBody] CreateRolDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingRol = await _rolService.GetRolDtoAsync(id);
        if (existingRol is null)
            return NotFound(new { message = $"Rol con id {id} no encontrado" });

        await _rolService.UpdateRolAsync(id, dto);

        return NoContent(); // 204
    }

    // DELETE: api/rol/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRol(int id)
    {
        var existingRol = await _rolService.GetRolDtoAsync(id);
        if (existingRol is null)
            return NotFound(new { message = $"Rol con id {id} no encontrado" });

        await _rolService.DeleteRolAsync(id);
        return NoContent();
    }
}
