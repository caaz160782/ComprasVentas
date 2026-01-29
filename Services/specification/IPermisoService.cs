using System;
using ComprasVentas.Dto;

namespace ComprasVentas.Services.specification;

public interface IPermisoService
{
    Task <List<PermisoDto>> GetAllPermisosAsync();
    Task <PermisoDto?> GetPermisoByIdAsync(int Id);
    Task <PermisoDto> CreatePermisoAsync(CreatePermisoDto dto);
}
