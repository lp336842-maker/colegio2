using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Colegio.Dominio.Contracts;
using Colegio.Dominio.Core;
using Colegio.Infraestructura.Context;

namespace Colegio.Infraestructura.Repositories;

public class Repositorio<T>(ContextoColegio context) : IRepositorio<T> where T : EntidadBase
{
    private readonly DbSet<T> _conjunto = context.Set<T>();

    public async Task<IReadOnlyList<T>> ObtenerTodosAsync(CancellationToken cancellationToken = default) =>
        await _conjunto.AsNoTracking().ToListAsync(cancellationToken);

    public Task<T?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        _conjunto.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<T> AgregarAsync(T entidad, CancellationToken cancellationToken = default)
    {
        _conjunto.Add(entidad);
        await context.SaveChangesAsync(cancellationToken);
        return entidad;
    }

    public async Task<bool> ActualizarAsync(T entidad, CancellationToken cancellationToken = default)
    {
        if (!await _conjunto.AnyAsync(x => x.Id == entidad.Id, cancellationToken)) return false;
        context.Entry(entidad).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        var entidad = await _conjunto.FindAsync([id], cancellationToken);
        if (entidad is null) return false;
        _conjunto.Remove(entidad);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<bool> ExisteAsync(Expression<Func<T, bool>> criterio, CancellationToken cancellationToken = default) =>
        _conjunto.AnyAsync(criterio, cancellationToken);
}
