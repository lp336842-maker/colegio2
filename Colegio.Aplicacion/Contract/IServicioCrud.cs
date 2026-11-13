using Colegio.Aplicacion.Core;

namespace Colegio.Aplicacion.Contract;

public interface IServicioCrud<TDto> where TDto : class
{
    Task<ResultadoServicio<IReadOnlyList<TDto>>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    Task<ResultadoServicio<TDto>> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ResultadoServicio<TDto>> CrearAsync(TDto dto, CancellationToken cancellationToken = default);
    Task<ResultadoServicio<TDto>> ActualizarAsync(int id, TDto dto, CancellationToken cancellationToken = default);
    Task<ResultadoServicio<bool>> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
