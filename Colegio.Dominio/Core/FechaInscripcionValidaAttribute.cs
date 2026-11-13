using System.ComponentModel.DataAnnotations;

namespace Colegio.Dominio.Core;

[AttributeUsage(AttributeTargets.Property)]
public sealed class FechaInscripcionValidaAttribute : ValidationAttribute
{
    public FechaInscripcionValidaAttribute() : base("La fecha de inscripción debe ser válida y no puede estar en el futuro.") { }

    public override bool IsValid(object? value) =>
        value is DateOnly fecha && fecha != default && fecha <= DateOnly.FromDateTime(DateTime.UtcNow);
}
