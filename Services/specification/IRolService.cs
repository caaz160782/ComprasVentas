using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;

namespace ComprasVentas.Services.specification;

public interface IRolService
{
    Task<List<Rol>> GetAllRolesAsync();
    Task<Rol> CreateRolAsync(CreateRolDto createRolDto);
}
