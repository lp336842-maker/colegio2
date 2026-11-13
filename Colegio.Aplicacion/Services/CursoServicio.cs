using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Core;
using Colegio.Aplicacion.Dtos.Curso;
using Colegio.Dominio.Contracts;
using Colegio.Dominio.Entities;

namespace Colegio.Aplicacion.Services;

public class CursoServicio(IRepositorio<Curso> repositorio, IRepositorio<Departamento> departamentos)
    : ServicioBase<Curso, CursoDto>(repositorio), ICursoServicio
{
    protected override Curso CrearEntidad(CursoDto dto) => new()
    {
        Codigo = dto.Codigo.Trim(),
        Nombre = dto.Nombre.Trim(),
        Creditos = dto.Creditos,
        DepartamentoId = dto.DepartamentoId
    };

    protected override void AplicarCambios(Curso entidad, CursoDto dto)
    {
        entidad.Codigo = dto.Codigo.Trim();
        entidad.Nombre = dto.Nombre.Trim();
        entidad.Creditos = dto.Creditos;
        entidad.DepartamentoId = dto.DepartamentoId;
    }

    protected override CursoDto CrearDto(Curso entidad) => new()
    {
        Id = entidad.Id,
        Codigo = entidad.Codigo,
        Nombre = entidad.Nombre,
        Creditos = entidad.Creditos,
        DepartamentoId = entidad.DepartamentoId
    };

    protected override async Task<string?> ValidarReglasAsync(CursoDto dto, int? id, CancellationToken cancellationToken)
    {
        if (await Repositorio.ExisteAsync(x => x.Codigo == dto.Codigo && (!id.HasValue || x.Id != id.Value), cancellationToken))
            return "Ya existe un curso con ese código.";
        if (!await departamentos.ExisteAsync(x => x.Id == dto.DepartamentoId, cancellationToken))
            return "El departamento indicado no existe.";
        return null;
    }
}
