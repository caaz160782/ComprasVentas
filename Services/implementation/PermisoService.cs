using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.specification;

namespace ComprasVentas.Services.implementation;

public class PermisoService (PermisoRepository permisoRepository): IPermisoService
{
    private readonly PermisoRepository _permisoRepository=permisoRepository;
    public async Task<List<PermisoDto>> GetAllPermisosAsync()
    {
      var permisos = await _permisoRepository.GetAllPermisosAsync();
      return permisos.Select(p => new PermisoDto
      {
          Id = p.Id,
         Nombre  = p.Nombre,
         Recurso = p.Recurso,
         Accion  = p.Accion
      }).ToList();
    }

    public async Task<PermisoDto?> GetPermisoByIdAsync(int Id)
    {
      var permiso = await _permisoRepository.GetPermisoByIdAsync(Id);
      if(permiso == null) return null;
      return new PermisoDto
        {  Id = permiso.Id,
            Nombre  = permiso.Nombre,
            Recurso = permiso.Recurso,
            Accion  = permiso.Accion
        };
    }
    public async Task<PermisoDto> CreatePermisoAsync(CreatePermisoDto dto)
    {
        var  permiso= new Permiso
        {   
            Nombre  = dto.Nombre,
            Recurso = dto.Recurso,
            Accion  = dto.Accion
        };
        await _permisoRepository.CreatePermisoAsync(permiso);

         return new PermisoDto
        {  Id = permiso.Id,
            Nombre  = permiso.Nombre,
            Recurso = permiso.Recurso,
            Accion  = permiso.Accion
        };
    }


}
