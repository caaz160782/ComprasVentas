using System;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Rol>Roles { get; set; }
    public DbSet<Permiso>Permisos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>()
            .HasMany(r=>Permisos)
            .WithMany(p=>Roles)
            .UsingEntity(q=> q.ToTable("permiso_rol"));
    }
}
