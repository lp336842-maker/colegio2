using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Core;
using Colegio.Aplicacion.Dtos.Estudiante;
using Colegio.Dominio.Contracts;
using Colegio.Dominio.Entities;

namespace Colegio.Aplicacion.Services;

public class EstudianteServicio(IRepositorio<Estudiante> repositorio)
    : ServicioBase<Estudiante, EstudianteDto>(repositorio), IEstudianteServicio
{
    protected override Estudiante CrearEntidad(EstudianteDto dto) => new()
    {
        Nombre = dto.Nombre.Trim(),
        Apellido = dto.Apellido.Trim(),
        Correo = dto.Correo.Trim(),
        FechaInscripcion = dto.FechaInscripcion
    };

    protected override void AplicarCambios(Estudiante entidad, EstudianteDto dto)
    {
        entidad.Nombre = dto.Nombre.Trim();
        entidad.Apellido = dto.Apellido.Trim();
        entidad.Correo = dto.Correo.Trim();
        entidad.FechaInscripcion = dto.FechaInscripcion;
    }

    protected override EstudianteDto CrearDto(Estudiante entidad) => new()
    {
        Id = entidad.Id,
        Nombre = entidad.Nombre,
        Apellido = entidad.Apellido,
        Correo = entidad.Correo,
        FechaInscripcion = entidad.FechaInscripcion
    };

    protected override async Task<string?> ValidarReglasAsync(EstudianteDto dto, int? id, CancellationToken cancellationToken) =>
        await Repositorio.ExisteAsync(x => x.Correo == dto.Correo && (!id.HasValue || x.Id != id.Value), cancellationToken)
            ? "Ya existe un estudiante con ese correo."
            : null;
}
