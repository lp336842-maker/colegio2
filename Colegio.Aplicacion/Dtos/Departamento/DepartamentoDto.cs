using System.ComponentModel.DataAnnotations;
using Colegio.Aplicacion.Dtos;

namespace Colegio.Aplicacion.Dtos.Departamento;

public class DepartamentoDto : IDtoEntidad
{
    [Range(1, int.MaxValue)]
    public int? Id { get; set; }

    [Required, StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Descripcion { get; set; }
}
