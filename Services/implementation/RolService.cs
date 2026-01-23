using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.specification;

namespace ComprasVentas.Services.implementation;

public class RolService : IRolService
{
     
    private readonly RolRepository _rolRepository;
    public RolService(RolRepository rolRepository)
    {
            _rolRepository = rolRepository;
    }
     public async Task<List<Rol>> GetAllRolesAsync()
    {
        return await _rolRepository.GetAllRolesAsync();
    }
    public async Task<Rol> CreateRolAsync(CreateRolDto createRolDto)
    {
        return await _rolRepository.CreateRolAsync(new Rol
        {
            Nombre = createRolDto.Nombre,
            Descripcion = createRolDto.Descripcion
        });
    }

   
}
