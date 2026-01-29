using System;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Repository;

public class RolRepository
{
    private readonly AppDbContext _context;

    public RolRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Rol>> GetAllRolesAsync()
    {
        return await _context.Roles.ToListAsync();        
    }

    public async Task<Rol?> GetRolByIdAsync(int id)
    {
        return await _context.Roles.Include(r => r.Permisos)
                                   .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Rol> CreateRolAsync(Rol rol)
    {
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();
        return rol;
    }

    public async Task<Rol?> UpdateRolAsync(int id, Rol updatedRol)
    {
        var existingRol = await _context.Roles.FindAsync(id);
        if (existingRol == null)
        {
            return null;
        }

        existingRol.Nombre = updatedRol.Nombre;
        existingRol.Descripcion = updatedRol.Descripcion;
        existingRol.Permisos = updatedRol.Permisos;

        await _context.SaveChangesAsync();
        return existingRol;
    }

    public async Task<bool> DeleteRolAsync(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol == null)
        {
            return false;
        }

        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();
        return true;
    }
}