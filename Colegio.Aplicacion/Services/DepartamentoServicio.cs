using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Core;
using Colegio.Aplicacion.Dtos.Departamento;
using Colegio.Dominio.Contracts;
using Colegio.Dominio.Entities;

namespace Colegio.Aplicacion.Services;

public class DepartamentoServicio(IRepositorio<Departamento> repositorio)
    : ServicioBase<Departamento, DepartamentoDto>(repositorio), IDepartamentoServicio
{
    protected override Departamento CrearEntidad(DepartamentoDto dto) => new()
    {
        Nombre = dto.Nombre.Trim(),
        Descripcion = dto.Descripcion?.Trim()
    };

    protected override void AplicarCambios(Departamento entidad, DepartamentoDto dto)
    {
        entidad.Nombre = dto.Nombre.Trim();
        entidad.Descripcion = dto.Descripcion?.Trim();
    }

    protected override DepartamentoDto CrearDto(Departamento entidad) => new()
    {
        Id = entidad.Id,
        Nombre = entidad.Nombre,
        Descripcion = entidad.Descripcion
    };

    protected override async Task<string?> ValidarReglasAsync(DepartamentoDto dto, int? id, CancellationToken cancellationToken) =>
        await Repositorio.ExisteAsync(x => x.Nombre == dto.Nombre && (!id.HasValue || x.Id != id.Value), cancellationToken)
            ? "Ya existe un departamento con ese nombre."
            : null;
}
