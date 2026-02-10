using System;
using System.Security.Principal;
using ComprasVentas.Dto;
using ComprasVentas.Exceptions;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.specification;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ComprasVentas.Services.implementation;

public class UsuarioService(UsuarioRepository usuarioRepository, RolRepository rolRepository) : IUsuarioService
{
    private readonly UsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly RolRepository _rolRepository = rolRepository;
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
            Nacionalidad = usuario.Persona?.Nacionalidad,    
            RolesIds =  usuario.Roles.Select(r=> r.Id).ToList()           
        };
    }


     public async Task<List<UsuarioDto>> GetAllUserAsync()
    {
        try
        {
            var usuarios = await _usuarioRepository.GetAllUserAsync();
            return usuarios.Select(MapToDto).ToList();    
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener los usuarios",ex);
        }
        
    }

    public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                throw new NotFoundException($"Usuario con ID {id} no encontrado");
            }

            return MapToDto(usuario);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener el usuario con ID {id}", ex);
        }
    }

    public async Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioDto createUsuarioDto)
    {
         try
        {
            var roles = new List<Rol>();
            if(createUsuarioDto.RolesIds  != null && createUsuarioDto.RolesIds.Count > 0)
            {
                foreach (var rolId in createUsuarioDto.RolesIds)
                {
                    var rol = await _rolRepository.GetRolByIdAsync(rolId);
                    if(rol != null) roles.Add(rol);
                } 
            }
            
            
            var usuario = new Usuario
            {
                Nombre = createUsuarioDto.Nombre,
                Correo = createUsuarioDto.Correo,
                Password = createUsuarioDto.Password,
                Persona = new Persona
                {
                    Nombres = createUsuarioDto.Nombres,
                    Apellidos = createUsuarioDto.Apellidos,
                    FechaNacimiento = createUsuarioDto.FechaNacimiento,
                    Genero = createUsuarioDto.Genero,
                    Telefono = createUsuarioDto.Telefono,
                    Direccion = createUsuarioDto.Direccion,
                    Nacionalidad = createUsuarioDto.Nacionalidad
                },
                Roles = roles
            };

            await _usuarioRepository.CreateUsuarioAsync(usuario);

            return MapToDto(usuario);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al crear el usuario", ex);
        }
    }

   public async Task UpdateUsuarioAsync(int id, CreateUsuarioDto updateUsuarioDto)
    {
        try
            {
                var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
                if (usuario == null)
                    throw new Exception($"Usuario con ID {id} no encontrado");

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

                if(updateUsuarioDto.RolesIds != null && updateUsuarioDto.RolesIds.Count >0)
                {
                    usuario.Roles.Clear();
                    foreach(var rolId in updateUsuarioDto.RolesIds )
                        {
                            var rol = await _rolRepository.GetRolByIdAsync(rolId);
                            if(rol != null)
                            {
                                usuario.Roles.Add(rol);
                            }   
                        }
                }
                await _usuarioRepository.UpdateUsuarioAsync(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario con ID {id}", ex);
            }
    }
  
   public async Task DeleteUsuarioAsync(int id)
    {
         try
        {
            await _usuarioRepository.DeleteUsuarioAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al eliminar el usuario con ID {id}", ex);
        }
    }
}
