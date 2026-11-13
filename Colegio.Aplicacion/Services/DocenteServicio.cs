using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Core;
using Colegio.Aplicacion.Dtos.Docente;
using Colegio.Dominio.Contracts;
using Colegio.Dominio.Entities;

namespace Colegio.Aplicacion.Services;

public class DocenteServicio(IRepositorio<Docente> repositorio, IRepositorio<Departamento> departamentos)
    : ServicioBase<Docente, DocenteDto>(repositorio), IDocenteServicio
{
    protected override Docente CrearEntidad(DocenteDto dto) => new()
    {
        Nombre = dto.Nombre.Trim(),
        Apellido = dto.Apellido.Trim(),
        Correo = dto.Correo.Trim(),
        DepartamentoId = dto.DepartamentoId
    };

    protected override void AplicarCambios(Docente entidad, DocenteDto dto)
    {
        entidad.Nombre = dto.Nombre.Trim();
        entidad.Apellido = dto.Apellido.Trim();
        entidad.Correo = dto.Correo.Trim();
        entidad.DepartamentoId = dto.DepartamentoId;
    }

    protected override DocenteDto CrearDto(Docente entidad) => new()
    {
        Id = entidad.Id,
        Nombre = entidad.Nombre,
        Apellido = entidad.Apellido,
        Correo = entidad.Correo,
        DepartamentoId = entidad.DepartamentoId
    };

    protected override async Task<string?> ValidarReglasAsync(DocenteDto dto, int? id, CancellationToken cancellationToken)
    {
        if (await Repositorio.ExisteAsync(x => x.Correo == dto.Correo && (!id.HasValue || x.Id != id.Value), cancellationToken))
            return "Ya existe un docente con ese correo.";
        if (!await departamentos.ExisteAsync(x => x.Id == dto.DepartamentoId, cancellationToken))
            return "El departamento indicado no existe.";
        return null;
    }
}
