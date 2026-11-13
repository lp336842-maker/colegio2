using System.ComponentModel.DataAnnotations;
using Colegio.Aplicacion.Dtos;

namespace Colegio.Aplicacion.Dtos.Estudiante;

public class EstudianteDto : IDtoEntidad
{
    [Range(1, int.MaxValue)]
    public int? Id { get; set; }

    [Required, StringLength(80, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, StringLength(80, MinimumLength = 2)]
    public string Apellido { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Correo { get; set; } = string.Empty;

    [CustomValidation(typeof(ValidacionesDto), nameof(ValidacionesDto.ValidarFechaInscripcion))]
    public DateOnly FechaInscripcion { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
}
