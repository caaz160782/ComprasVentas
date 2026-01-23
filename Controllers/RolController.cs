using ComprasVentas.Dto;
using ComprasVentas.Services.specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _rolService.GetAllRolesAsync();
            return Ok(roles);       
        }

        [HttpPost]
        public async Task<IActionResult> CreateRol([FromBody] CreateRolDto createRolDto)
        {
            var rol = await _rolService.CreateRolAsync(createRolDto);
            return CreatedAtAction(nameof(GetAllRoles), new { id = rol.Id }, rol);      
        }   
    }
}
