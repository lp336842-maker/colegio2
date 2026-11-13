# Segunda parte: servicios de colegio

Solución independiente en .NET 10 con los proyectos `Colegio.Dominio`, `Colegio.Infraestructura`, `Colegio.Aplicacion` y `Colegio.Api`. Esta carpeta no modifica el proyecto de la primera parte.

La capa de Aplicación contiene `Contract`, `Dtos`, `Core` y `Services`. Hay un servicio de negocio para cursos, departamentos, estudiantes y docentes. Los campos se validan con atributos de datos en los DTO y las entidades; los servicios también comprueban códigos, correos, nombres únicos y departamentos existentes.

## Inicio

Desde esta carpeta:

```powershell
dotnet run --project Colegio.Api --launch-profile http
```

La API usa SQLite en `colegio-segunda-parte.db`. El perfil HTTP abre Swagger en [http://localhost:5130/swagger](http://localhost:5130/swagger), y la raíz [http://localhost:5130](http://localhost:5130) redirige a esa interfaz.

Las operaciones CRUD que se completan correctamente responden con HTTP 200 y un cuerpo JSON. Los datos inválidos responden 400 y los identificadores que no existen responden 404.

## Recursos

- `/api/cursos`
- `/api/departamentos`
- `/api/estudiantes`
- `/api/docentes`

Cada recurso permite consultar la colección, consultar por identificador, crear, actualizar y eliminar. Para crear cursos o docentes, primero utiliza un departamento existente; la base nueva agrega el departamento `General`.

## Estructura de Aplicación

```text
Colegio.Aplicacion/
  Contract/
  Core/
  Dtos/
    Curso/
    Departamento/
    Docente/
    Estudiante/
  Services/
```
