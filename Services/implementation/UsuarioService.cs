using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.specification;

namespace ComprasVentas.Services.implementation;

public class UsuarioService(UsuarioRepository usuarioRepository) : IUsuarioService
{
    private readonly UsuarioRepository _usuarioRepository = usuarioRepository;

     public async Task<List<UsuarioDto>> GetAllUserAsync()
    {
        var usuarios = await _usuarioRepository.GetAllUserAsync();
        return usuarios.Select(MapToDto).ToList();
    }

    public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);

        if(usuario == null) return null;
        return MapToDto(usuario);
    }


    public async Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioDto createUsuarioDto)
    {
        var usuario = new Usuario
        {
            Nombre = createUsuarioDto.Nombre,
            Correo = createUsuarioDto.Correo,
            Password= createUsuarioDto.Password,
            Persona = new Persona
            {
                Nombres = createUsuarioDto.Nombres,
                Apellidos = createUsuarioDto.Apellidos,
                FechaNacimiento = createUsuarioDto.FechaNacimiento,
                Genero = createUsuarioDto.Genero,
                Telefono = createUsuarioDto.Telefono,
                Direccion = createUsuarioDto.Direccion,
                Nacionalidad = createUsuarioDto.Nacionalidad
            }
        };
        
        await _usuarioRepository.CreateUsuarioAsync(usuario);

        return  MapToDto(usuario);
    }

   public async Task UpdateUsuarioAsync(int id, CreateUsuarioDto updateUsuarioDto)
{
    var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
    if (usuario == null) return;

    usuario.Nombre = updateUsuarioDto.Nombre;
    usuario.Correo = updateUsuarioDto.Correo;
    usuario.Password = updateUsuarioDto.Password;

    if (usuario.Persona == null)
        throw new InvalidOperationException("El usuario no tiene Persona asociada.");

    usuario.Persona.Nombres = updateUsuarioDto.Nombres;
    usuario.Persona.Apellidos = updateUsuarioDto.Apellidos;
    usuario.Persona.FechaNacimiento = updateUsuarioDto.FechaNacimiento;
    usuario.Persona.Genero = updateUsuarioDto.Genero;
    usuario.Persona.Telefono = updateUsuarioDto.Telefono;
    usuario.Persona.Direccion = updateUsuarioDto.Direccion;
    usuario.Persona.Nacionalidad = updateUsuarioDto.Nacionalidad;

    await _usuarioRepository.UpdateUsuarioAsync(usuario);
}

    public async Task DeleteUsuarioAsync(int id)
    {
        await _usuarioRepository.DeleteUsuarioAsync(id);
    } 


    private UsuarioDto MapToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo,
            Nombres = usuario.Persona?.Nombres ?? string.Empty,
            Apellidos = usuario.Persona?.Apellidos ?? string.Empty,
             FechaNacimiento = usuario.Persona?.FechaNacimiento ?? DateTime.MinValue,
            Genero = usuario.Persona?.Genero ,
            Telefono = usuario.Persona?.Telefono,
            Direccion = usuario.Persona?.Direccion,
            Nacionalidad = usuario.Persona?.Nacionalidad
                
        };
    }
}
