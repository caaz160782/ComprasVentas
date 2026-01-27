using System;
using ComprasVentas.Models;

namespace ComprasVentas.Builder;

public class RoleBuilder
{
    private readonly Rol _rol =new Rol();

    public RoleBuilder WithNombre(string nombre)
    {
        _rol.Nombre=nombre;
        return this;
    }

    public RoleBuilder WithDescripcion(string descripcion)
    {
        _rol.Descripcion=descripcion;
        return this;
    }

    public RoleBuilder WithPermisos(List<Permiso> permisos)
    {
        _rol.Permisos=permisos;
        return this;
    }

    public Rol Build()
    {
        return _rol;
    }

}
