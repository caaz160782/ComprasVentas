using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;

namespace ComprasVentas.Services.specification;

public interface IRolService
{
    Task<List<RolDto>> GetAllRolesAsync();
    Task<RolDto?> GetRolDtoAsync(int id);
    Task<RolDto> CreateRolAsync(CreateRolDto dto);
    Task UpdateRolAsync(int id, CreateRolDto dto);
    Task DeleteRolAsync(int id);
}
