using System.ComponentModel.DataAnnotations;
using Colegio.Aplicacion.Dtos;

namespace Colegio.Aplicacion.Dtos.Curso;

public class CursoDto : IDtoEntidad
{
    [Range(1, int.MaxValue)]
    public int? Id { get; set; }

    [Required, StringLength(20, MinimumLength = 3)]
    [RegularExpression("^[A-Za-z0-9-]+$")]
    public string Codigo { get; set; } = string.Empty;

    [Required, StringLength(150, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Range(1, 20)]
    public int Creditos { get; set; }

    [Range(1, int.MaxValue)]
    public int DepartamentoId { get; set; }
}
