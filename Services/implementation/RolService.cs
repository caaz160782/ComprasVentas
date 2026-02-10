using System;
using ComprasVentas.Builder;
using ComprasVentas.Dto;
using ComprasVentas.Exceptions;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.specification;

namespace ComprasVentas.Services.implementation;

public class RolService(RolRepository rolRepository, PermisoRepository permisoRepository) : IRolService
{
     
    private readonly RolRepository _rolRepository=rolRepository;
    private readonly PermisoRepository _permisoRepository=permisoRepository;

    public async Task<List<RolDto>> GetAllRolesAsync()
    {
       var roles = await _rolRepository.GetAllRolesAsync();
       return [.. roles.Select(r => new RolDto
       {
        Id = r.Id,
        Nombre = r.Nombre,
        Descripcion = r.Descripcion,
        PermisosIds = r.Permisos.Select(p => p.Id).ToList() ??[]        
       })]; 
    }
    public async Task<RolDto?> GetRolDtoAsync(int id)
    {
        var rol = await _rolRepository.GetRolByIdAsync(id);
        if(rol == null)  throw new NotFoundException($"Rol con ID {id} no encontrado");

        return new RolDto
        {
            Id= rol.Id,
            Nombre= rol.Nombre,
            Descripcion= rol.Descripcion,
            PermisosIds= rol.Permisos.Select(p => p.Id).ToList() ?? []
        };
    }
    public async Task<RolDto> CreateRolAsync(CreateRolDto dto)
    {
      var permisos = new List<Permiso>();
      foreach( var permisoId in dto.PermisosIds)
        {
            var permiso = await _permisoRepository.GetPermisoByIdAsync(permisoId);
            if(permiso != null) permisos.Add(permiso);
        }
       var rol = new RoleBuilder()
                 .WithNombre(dto.Nombre)
                 .WithDescripcion(dto.Descripcion)
                 .WithPermisos(permisos)
                 .Build();
        await _rolRepository.CreateRolAsync(rol);

        var rolResponse = new RolDto
        {
            Id= rol.Id,
            Nombre = rol.Nombre,
            Descripcion = rol.Descripcion,
            PermisosIds = rol.Permisos?.Select(p => p.Id).ToList() ??[]
        };

        return rolResponse;
    }    
    public async Task UpdateRolAsync(int id, CreateRolDto dto)
    {
        var rol = await _rolRepository.GetRolByIdAsync(id);
        if(rol == null ) return;
        
        rol.Nombre= dto.Nombre;
        rol.Descripcion =dto.Descripcion;
        rol.Permisos.Clear();

        foreach(var permisoId in dto.PermisosIds)
        {
            var permiso = await _permisoRepository.GetPermisoByIdAsync(permisoId);
            if(permiso != null) rol.Permisos.Add(permiso);
        }

    
        await _rolRepository.UpdateRolAsync(id,rol);

    }
    public async Task DeleteRolAsync(int id)
    {
        await _rolRepository.DeleteRolAsync(id);
    }
   
}
