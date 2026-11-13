using Microsoft.AspNetCore.Mvc;
using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Dtos.Curso;

namespace Colegio.Api.Controllers;

[ApiController]
[Route("api/cursos")]
public class CursosController(ICursoServicio servicio) : ControladorCrudBase<CursoDto>(servicio) { }
