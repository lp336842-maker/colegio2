using System.ComponentModel.DataAnnotations;

namespace Colegio.Aplicacion.Dtos;

public static class ValidacionesDto
{
    public static ValidationResult? ValidarFechaInscripcion(DateOnly fecha, ValidationContext context)
    {
        if (fecha == default) return new ValidationResult("La fecha de inscripción es obligatoria.");
        if (fecha > DateOnly.FromDateTime(DateTime.UtcNow)) return new ValidationResult("La fecha de inscripción no puede estar en el futuro.");
        return ValidationResult.Success;
    }
}
