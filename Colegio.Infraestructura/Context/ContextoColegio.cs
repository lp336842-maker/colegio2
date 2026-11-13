using Microsoft.EntityFrameworkCore;
using Colegio.Dominio.Entities;

namespace Colegio.Infraestructura.Context;

public class ContextoColegio(DbContextOptions<ContextoColegio> options) : DbContext(options)
{
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Docente> Docentes => Set<Docente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>().HasIndex(x => x.Codigo).IsUnique();
        modelBuilder.Entity<Departamento>().HasIndex(x => x.Nombre).IsUnique();
        modelBuilder.Entity<Estudiante>().HasIndex(x => x.Correo).IsUnique();
        modelBuilder.Entity<Docente>().HasIndex(x => x.Correo).IsUnique();
        modelBuilder.Entity<Curso>().HasOne(x => x.Departamento).WithMany(x => x.Cursos).HasForeignKey(x => x.DepartamentoId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Docente>().HasOne(x => x.Departamento).WithMany(x => x.Docentes).HasForeignKey(x => x.DepartamentoId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Curso>().HasMany(x => x.Estudiantes).WithMany(x => x.Cursos);
        modelBuilder.Entity<Curso>().HasMany(x => x.Docentes).WithMany(x => x.Cursos);
    }
}
