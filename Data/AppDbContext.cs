using System;
using System.Collections.Generic;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Rol> Roles { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===============================
        // MANY TO MANY: ROL - PERMISO
        // ===============================
        modelBuilder.Entity<Rol>()
            .HasMany(r => r.Permisos)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "permiso_rol",
                j => j
                    .HasOne<Permiso>()
                    .WithMany()
                    .HasForeignKey("PermisoId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Rol>()
                    .WithMany()
                    .HasForeignKey("RolId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("PermisoId", "RolId");
                    j.ToTable("permiso_rol");
                });

        // ===============================
        // MANY TO MANY: USUARIO - ROL
        // ===============================
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Usuarios)
            .UsingEntity<Dictionary<string, object>>(
                "usuario_rol",
                j => j
                    .HasOne<Rol>()
                    .WithMany()
                    .HasForeignKey("RolId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey("UsuarioId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("UsuarioId", "RolId");
                    j.ToTable("usuario_rol");
                });

        // ===============================
        // ONE TO ONE: USUARIO - PERSONA
        // ===============================
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Persona)
            .WithOne(p => p.Usuario)
            .HasForeignKey<Persona>(p => p.Id);

        // ===============================
        // CONFIGURACIONES
        // ===============================
        modelBuilder.Entity<Usuario>(e =>
        {
            e.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
            e.Property(u => u.Correo).IsRequired().HasMaxLength(255);
            e.Property(u => u.Password).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Persona>(e =>
        {
            e.Property(p => p.Nombres).IsRequired().HasMaxLength(100);
            e.Property(p => p.Apellidos).IsRequired().HasMaxLength(100);
            e.Property(p => p.Genero).IsRequired().HasMaxLength(20);
            e.Property(p => p.Telefono).IsRequired().HasMaxLength(20);
            e.Property(p => p.Direccion).IsRequired().HasMaxLength(255);
            e.Property(p => p.Nacionalidad).IsRequired().HasMaxLength(50);

            // 🔥 CORRECTO PARA POSTGRESQL
            e.Property(p => p.FechaNacimiento)
             .HasColumnType("date");
        });

        modelBuilder.Entity<Permiso>(e =>
        {
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            e.Property(p => p.Accion).IsRequired().HasMaxLength(100);
            e.Property(p => p.Recurso).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Rol>(e =>
        {
            e.Property(r => r.Nombre).IsRequired().HasMaxLength(100);
            e.Property(r => r.Descripcion).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<RefreshToken>(e =>
        {
            e.Property(r => r.Token).IsRequired().HasMaxLength(500);
            e.HasOne(r => r.Usuario)
             .WithMany(u => u.RefreshTokens)
             .HasForeignKey(r => r.UsuarioId);
        });

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // ===============================
        // PERMISOS
        // ===============================
        modelBuilder.Entity<Permiso>().HasData(
            new Permiso { Id = 1, Nombre = "ROL_CREATE", Recurso = "ROL", Accion = "CREATE" },
            new Permiso { Id = 2, Nombre = "ROL_READ", Recurso = "ROL", Accion = "READ" },
            new Permiso { Id = 3, Nombre = "ROL_UPDATE", Recurso = "ROL", Accion = "UPDATE" },
            new Permiso { Id = 4, Nombre = "ROL_DELETE", Recurso = "ROL", Accion = "DELETE" }
        );

        // ===============================
        // ROLES
        // ===============================
        modelBuilder.Entity<Rol>().HasData(
            new Rol { Id = 1, Nombre = "Super Administrador", Descripcion = "Acceso completo" },
            new Rol { Id = 2, Nombre = "Administrador", Descripcion = "Gestión del sistema" }
        );

        // ===============================
        // RELACION ROL-PERMISO
        // ===============================
        modelBuilder.Entity("permiso_rol").HasData(
            new { PermisoId = 1, RolId = 1 },
            new { PermisoId = 2, RolId = 1 },
            new { PermisoId = 3, RolId = 1 },
            new { PermisoId = 4, RolId = 1 }
        );

        // ===============================
        // PERSONAS
        // ===============================
        modelBuilder.Entity<Persona>().HasData(
            new Persona
            {
                Id = 1,
                Nombres = "Juan",
                Apellidos = "Pérez",
                FechaNacimiento =new DateTime(2026, 2, 12, 0, 0, 0, DateTimeKind.Utc),
                Genero = "Masculino",
                Telefono = "+1234567890",
                Direccion = "Calle Principal 123",
                Nacionalidad = "Mexicana"
            }
        );

        // ===============================
        // USUARIOS
        // ===============================
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                Nombre = "superadmin",
                Correo = "superadmin@comprasventas.com",
                Password = "$2a$11$wX0W9Dk3KQ7vQ9y0JH1Y8uRzZ0xJzjW6kX3KZlM1WvP7QpVn5L6rS"
            }
        );

        // ===============================
        // RELACION USUARIO-ROL
        // ===============================
        modelBuilder.Entity("usuario_rol").HasData(
            new { UsuarioId = 1, RolId = 1 }
        );
    }
}
