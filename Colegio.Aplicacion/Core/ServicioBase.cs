using System.ComponentModel.DataAnnotations;
using System.Net;
using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Dtos;
using Colegio.Dominio.Contracts;
using Colegio.Dominio.Core;

namespace Colegio.Aplicacion.Core;

public abstract class ServicioBase<TEntidad, TDto>(IRepositorio<TEntidad> repositorio) : IServicioCrud<TDto>
    where TEntidad : EntidadBase
    where TDto : class, IDtoEntidad
{
    protected readonly IRepositorio<TEntidad> Repositorio = repositorio;

    protected abstract TEntidad CrearEntidad(TDto dto);
    protected abstract void AplicarCambios(TEntidad entidad, TDto dto);
    protected abstract TDto CrearDto(TEntidad entidad);

    protected virtual Task<string?> ValidarReglasAsync(TDto dto, int? id, CancellationToken cancellationToken) => Task.FromResult<string?>(null);

    public async Task<ResultadoServicio<IReadOnlyList<TDto>>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        var entidades = await Repositorio.ObtenerTodosAsync(cancellationToken);
        return ResultadoServicio<IReadOnlyList<TDto>>.Ok(entidades.Select(CrearDto).ToList(), "Registros obtenidos.");
    }

    public async Task<ResultadoServicio<TDto>> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entidad = await Repositorio.ObtenerPorIdAsync(id, cancellationToken);
        return entidad is null
            ? ResultadoServicio<TDto>.Error("No se encontró el registro.", (int)HttpStatusCode.NotFound)
            : ResultadoServicio<TDto>.Ok(CrearDto(entidad), "Registro obtenido.");
    }

    public async Task<ResultadoServicio<TDto>> CrearAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var errores = Validar(dto);
        if (errores.Count > 0) return ResultadoServicio<TDto>.Error("Los datos no son válidos.", (int)HttpStatusCode.BadRequest, errores);

        var errorRegla = await ValidarReglasAsync(dto, null, cancellationToken);
        if (errorRegla is not null) return ResultadoServicio<TDto>.Error(errorRegla, (int)HttpStatusCode.BadRequest);

        var entidad = CrearEntidad(dto);
        errores = Validar(entidad);
        if (errores.Count > 0) return ResultadoServicio<TDto>.Error("La entidad no es válida.", (int)HttpStatusCode.BadRequest, errores);

        var guardada = await Repositorio.AgregarAsync(entidad, cancellationToken);
        return ResultadoServicio<TDto>.Ok(CrearDto(guardada), "Registro creado.");
    }

    public async Task<ResultadoServicio<TDto>> ActualizarAsync(int id, TDto dto, CancellationToken cancellationToken = default)
    {
        if (id < 1) return ResultadoServicio<TDto>.Error("El identificador debe ser mayor que cero.", (int)HttpStatusCode.BadRequest);
        if (dto.Id.HasValue && dto.Id.Value != id) return ResultadoServicio<TDto>.Error("El identificador de la ruta no coincide con el del cuerpo.", (int)HttpStatusCode.BadRequest);

        var errores = Validar(dto);
        if (errores.Count > 0) return ResultadoServicio<TDto>.Error("Los datos no son válidos.", (int)HttpStatusCode.BadRequest, errores);

        var errorRegla = await ValidarReglasAsync(dto, id, cancellationToken);
        if (errorRegla is not null) return ResultadoServicio<TDto>.Error(errorRegla, (int)HttpStatusCode.BadRequest);

        var entidad = CrearEntidad(dto);
        entidad.Id = id;
        AplicarCambios(entidad, dto);
        errores = Validar(entidad);
        if (errores.Count > 0) return ResultadoServicio<TDto>.Error("La entidad no es válida.", (int)HttpStatusCode.BadRequest, errores);

        if (!await Repositorio.ActualizarAsync(entidad, cancellationToken))
            return ResultadoServicio<TDto>.Error("No se encontró el registro.", (int)HttpStatusCode.NotFound);

        return ResultadoServicio<TDto>.Ok(CrearDto(entidad), "Registro actualizado.");
    }

    public async Task<ResultadoServicio<bool>> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1) return ResultadoServicio<bool>.Error("El identificador debe ser mayor que cero.", (int)HttpStatusCode.BadRequest);
        return await Repositorio.EliminarAsync(id, cancellationToken)
            ? ResultadoServicio<bool>.Ok(true, "Registro eliminado.")
            : ResultadoServicio<bool>.Error("No se encontró el registro.", (int)HttpStatusCode.NotFound);
    }

    private static List<string> Validar(object objeto)
    {
        var resultados = new List<ValidationResult>();
        Validator.TryValidateObject(objeto, new ValidationContext(objeto), resultados, true);
        return resultados.Select(x => x.ErrorMessage ?? "El valor no es válido.").ToList();
    }
}
