using System;
using ComprasVentas.Dto;

namespace ComprasVentas.Services.specification;

public interface IUsuarioService
{
    Task <List<UsuarioDto>> GetAllUserAsync();
    Task <UsuarioDto?> GetUsuarioByIdAsync(int id);
    Task <UsuarioDto> CreateUsuarioAsync(CreateUsuarioDto createUsuarioDto);
    Task UpdateUsuarioAsync(int id, CreateUsuarioDto updateUsuarioDto);
    Task DeleteUsuarioAsync(int id);

}
