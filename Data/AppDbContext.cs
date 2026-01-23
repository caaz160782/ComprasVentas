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
        modelBuilder.Entity<Rol>(entity =>
    {
        entity.ToTable("roles");
        entity.HasKey(r => r.Id);
        entity.Property(r => r.Id).HasColumnName("id");
    });

    modelBuilder.Entity<Permiso>(entity =>
    {
        entity.ToTable("permisos");
        entity.HasKey(p => p.Id);
        entity.Property(p => p.Id).HasColumnName("id");
    });

        modelBuilder.Entity<Rol>()
        .HasMany(r => r.Permisos)
        .WithMany(p => p.Roles)
        .UsingEntity<Dictionary<string, object>>(
            "permiso_role",
            j => j
                .HasOne<Permiso>()
                .WithMany()
                .HasForeignKey("permiso_id")
                .HasConstraintName("fk_permiso_rol_permiso"),
            j => j
                .HasOne<Rol>()
                .WithMany()
                .HasForeignKey("rol_id")
                .HasConstraintName("fk_permiso_rol_rol"),
            j =>
            {
                j.ToTable("permiso_role");
                j.HasKey("permiso_id", "rol_id");
            });
    }
}
