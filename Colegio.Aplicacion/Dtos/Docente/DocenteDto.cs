using System.ComponentModel.DataAnnotations;
using Colegio.Aplicacion.Dtos;

namespace Colegio.Aplicacion.Dtos.Docente;

public class DocenteDto : IDtoEntidad
{
    [Range(1, int.MaxValue)]
    public int? Id { get; set; }

    [Required, StringLength(80, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, StringLength(80, MinimumLength = 2)]
    public string Apellido { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Correo { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DepartamentoId { get; set; }
}
