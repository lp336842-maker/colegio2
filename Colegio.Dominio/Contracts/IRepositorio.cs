using System.Linq.Expressions;
using Colegio.Dominio.Core;

namespace Colegio.Dominio.Contracts;

public interface IRepositorio<T> where T : EntidadBase
{
    Task<IReadOnlyList<T>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    Task<T?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> AgregarAsync(T entidad, CancellationToken cancellationToken = default);
    Task<bool> ActualizarAsync(T entidad, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExisteAsync(Expression<Func<T, bool>> criterio, CancellationToken cancellationToken = default);
}
