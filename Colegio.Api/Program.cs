using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Colegio.Aplicacion.Contract;
using Colegio.Aplicacion.Services;
using Colegio.Dominio.Contracts;
using Colegio.Dominio.Entities;
using Colegio.Infraestructura.Context;
using Colegio.Infraestructura.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de gestión del colegio",
        Version = "v1",
        Description = "API para cursos, departamentos, estudiantes y docentes con validaciones y servicios de negocio."
    });
});

var connectionString = builder.Configuration.GetConnectionString("Colegio") ?? "Data Source=colegio-segunda-parte.db";
builder.Services.AddDbContext<ContextoColegio>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<ICursoServicio, CursoServicio>();
builder.Services.AddScoped<IDepartamentoServicio, DepartamentoServicio>();
builder.Services.AddScoped<IEstudianteServicio, EstudianteServicio>();
builder.Services.AddScoped<IDocenteServicio, DocenteServicio>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API del colegio v1");
    options.RoutePrefix = "swagger";
    options.DocumentTitle = "API del colegio | Swagger";
});

app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ContextoColegio>();
    await context.Database.EnsureCreatedAsync();
    if (!await context.Departamentos.AnyAsync())
    {
        context.Departamentos.Add(new Departamento { Nombre = "General" });
        await context.SaveChangesAsync();
    }
}

app.Run();
