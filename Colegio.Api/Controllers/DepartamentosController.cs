using Microsoft.AspNetCore.Mvc;
using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Dtos.Departamento;

namespace Colegio.Api.Controllers;

[ApiController]
[Route("api/departamentos")]
public class DepartamentosController(IDepartamentoServicio servicio) : ControladorCrudBase<DepartamentoDto>(servicio) { }
