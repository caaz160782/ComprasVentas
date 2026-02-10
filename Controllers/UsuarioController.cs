using ComprasVentas.Dto;
using ComprasVentas.Exceptions;
using ComprasVentas.Services.specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDto>>> GetAll()
        {
            return Ok(await _usuarioService.GetAllUserAsync());
        } 
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
           
           var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
           return Ok(usuario);            
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Create(CreateUsuarioDto usuarioCreateDto)
        {
            var createdUsuario = await _usuarioService.CreateUsuarioAsync(usuarioCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto>> Update(int id, CreateUsuarioDto usuarioUpdateDto)
        {
          await _usuarioService.UpdateUsuarioAsync(id, usuarioUpdateDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)        {
            await _usuarioService.DeleteUsuarioAsync(id);
            return NoContent(); 
        }



    }
}
