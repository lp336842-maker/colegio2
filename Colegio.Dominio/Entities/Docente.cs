using System.ComponentModel.DataAnnotations;
using Colegio.Dominio.Core;

namespace Colegio.Dominio.Entities;

public class Docente : EntidadBase
{
    [Required, StringLength(80, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, StringLength(80, MinimumLength = 2)]
    public string Apellido { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Correo { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DepartamentoId { get; set; }

    public Departamento? Departamento { get; set; }
    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
