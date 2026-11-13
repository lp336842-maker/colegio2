using Microsoft.AspNetCore.Mvc;
using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Dtos.Estudiante;

namespace Colegio.Api.Controllers;

[ApiController]
[Route("api/estudiantes")]
public class EstudiantesController(IEstudianteServicio servicio) : ControladorCrudBase<EstudianteDto>(servicio) { }
