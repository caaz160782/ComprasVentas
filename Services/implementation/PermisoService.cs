using System;
using ComprasVentas.Dto;
using ComprasVentas.Repository;
using ComprasVentas.Services.specification;

namespace ComprasVentas.Services.implementation;

public class PermisoService (PermisoRepository permisoRepository): IPermisoService
{
    private readonly PermisoRepository _permisoRepository=permisoRepository;
    public Task<List<PermisoDto>> GetAllPermisosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PermisoDto?> GetPermisoByIdAsync(int Id)
    {
        throw new NotImplementedException();
    }
    public Task<PermisoDto> CreatePermisoAsync(CreatePermisoDto dto)
    {
        throw new NotImplementedException();
    }


}
