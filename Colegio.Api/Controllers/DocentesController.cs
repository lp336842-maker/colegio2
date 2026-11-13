using Microsoft.AspNetCore.Mvc;
using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Dtos.Docente;

namespace Colegio.Api.Controllers;

[ApiController]
[Route("api/docentes")]
public class DocentesController(IDocenteServicio servicio) : ControladorCrudBase<DocenteDto>(servicio) { }
