using Microsoft.AspNetCore.Mvc;
using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Core;
using Colegio.Aplicacion.Dtos;

namespace Colegio.Api.Controllers;

public abstract class ControladorCrudBase<TDto>(IServicioCrud<TDto> servicio) : ControllerBase
    where TDto : class, IDtoEntidad
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken) =>
        Responder(await servicio.ObtenerTodosAsync(cancellationToken));

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken) =>
        Responder(await servicio.ObtenerPorIdAsync(id, cancellationToken));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] TDto dto, CancellationToken cancellationToken) =>
        Responder(await servicio.CrearAsync(dto, cancellationToken));

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] TDto dto, CancellationToken cancellationToken) =>
        Responder(await servicio.ActualizarAsync(id, dto, cancellationToken));

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(int id, CancellationToken cancellationToken) =>
        Responder(await servicio.EliminarAsync(id, cancellationToken));

    private IActionResult Responder<T>(ResultadoServicio<T> resultado) =>
        StatusCode(resultado.CodigoHttp, resultado);
}
