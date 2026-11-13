using System.ComponentModel.DataAnnotations;
using Colegio.Dominio.Core;

namespace Colegio.Dominio.Entities;

public class Departamento : EntidadBase
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Descripcion { get; set; }

    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    public ICollection<Docente> Docentes { get; set; } = new List<Docente>();
}
