using System.Net;

namespace Colegio.Aplicacion.Core;

public sealed record ResultadoServicio<T>(bool Exitoso, string Mensaje, T? Datos, IReadOnlyList<string> Errores, int CodigoHttp)
{
    public static ResultadoServicio<T> Ok(T? datos, string mensaje) => new(true, mensaje, datos, [], (int)HttpStatusCode.OK);
    public static ResultadoServicio<T> Error(string mensaje, int codigoHttp, IEnumerable<string>? errores = null) => new(false, mensaje, default, errores?.ToArray() ?? [], codigoHttp);
}
