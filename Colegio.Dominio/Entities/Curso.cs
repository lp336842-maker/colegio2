using System.ComponentModel.DataAnnotations;
using Colegio.Dominio.Core;

namespace Colegio.Dominio.Entities;

public class Curso : EntidadBase
{
    [Required, StringLength(20, MinimumLength = 3)]
    [RegularExpression("^[A-Za-z0-9-]+$")]
    public string Codigo { get; set; } = string.Empty;

    [Required, StringLength(150, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Range(1, 20)]
    public int Creditos { get; set; }

    [Range(1, int.MaxValue)]
    public int DepartamentoId { get; set; }

    public Departamento? Departamento { get; set; }
    public ICollection<Docente> Docentes { get; set; } = new List<Docente>();
    public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
