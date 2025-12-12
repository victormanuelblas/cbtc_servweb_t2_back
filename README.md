# Backend - servweb1_t2

Este repositorio contiene el backend del examen `servweb1_t2` organizado en 4 proyectos dentro de `src`:

- `servweb1_t2.Domain` - Entidades del dominio y puertos (interfaces).
- `servweb1_t2.Application` - Lógica de aplicación, servicios, DTOs y mapeos.
- `servweb1_t2.Infrastructure` - Implementaciones de persistencia, `ApplicationDbContext` y repositorios.
- `servweb1_t2.Api` - Proyecto Web API (startup / endpoints).

## Requisitos

- .NET 8 SDK (o la versión que el proyecto requiere)
- MySQL (o MariaDB) accesible para aplicar migraciones y ejecutar la app
- `dotnet-ef` herramienta instalada globalmente o disponible (`dotnet tool install --global dotnet-ef`)

## Variables de entorno

El proyecto de `Infrastructure` lee la conexión a la BD desde variables de entorno (se configuran en `servweb1_t2.Infrastructure/DependencyInjection.cs`). Las variables esperadas son:

- `BD_HOST` (host del servidor MySQL)
- `DB_PORT` (puerto, ej. `3306`)
- `DB_NAME` (nombre de la BD)
- `DB_USER` (usuario)
- `DB_PASSWORD` (contraseña)

Ejemplo (Linux/macOS zsh):

    export BD_HOST=localhost
    export DB_PORT=3306
    export DB_NAME=library_db
    export DB_USER=root
    export DB_PASSWORD='abc123DEF'

> Nota: en el repo de ejemplo hay un archivo `.env` con una muestra de estos valores. No lo subas a repositorios públicos con datos reales.

## Migraciones (Entity Framework)

Las migraciones se generan en el proyecto `servweb1_t2.Infrastructure`. Para crear una migración nueva:

    # desde la raíz del repo
    dotnet ef migrations add NombreDeLaMigracion -p src/servweb1_t2.Infrastructure -s src/servweb1_t2.Api --context ApplicationDbContext -o Migrations

Para aplicar las migraciones a la base de datos (ejecutar `UPDATE`):

    # asegúrate de exportar las variables BD_* antes
    dotnet ef database update -p src/servweb1_t2.Infrastructure -s src/servweb1_t2.Api --context ApplicationDbContext

En este repositorio se añadió la migración `AddArticulosBajaLiquidacion` que crea las tablas `ArticulosBaja` y `ArticulosLiquidacion`.

## Construir y ejecutar

Para compilar la solución completa:

    dotnet build servweb1_t2.sln

Para ejecutar únicamente la API (p. ej. para desarrollo):

    cd src/servweb1_t2.Api
    # exportar variables de conexión primero
    export BD_HOST=localhost DB_PORT=3306 DB_NAME=library_db DB_USER=root DB_PASSWORD='abc123DEF'
    # ejecutar
    dotnet run --urls "http://localhost:5004"

La API por defecto escucha en `http://localhost:5004` según `launchSettings.json`.

## Endpoints principales

Resumen rápido de los endpoints implementados por el examen (ver `Controllers` para detalles):

- Libros
  - `GET /api/Book` - Listar libros
  - `GET /api/Book/{id}` - Obtener libro por id
  - `POST /api/Book` - Crear libro
  - `PUT /api/Book/{id}` - Actualizar libro
  - `DELETE /api/Book/{id}` - Eliminar libro
  - `POST /api/Book/{id}/dar-baja` - Dar de baja un artículo: crea registro en `ArticulosBaja`, si el stock > 0 crea registro en `ArticulosLiquidacion` y elimina el libro (todo en una transacción)

- Préstamos
  - `GET /api/Loan` - Listar préstamos
  - `GET /api/Loan/{id}` - Obtener préstamo
  - `POST /api/Loan` - Crear préstamo (body: `{ bookId, studentName }`) - la API y/o frontend validan stock
  - `PUT /api/Loan/{id}` - Actualizar préstamo
  - `DELETE /api/Loan/{id}` - Eliminar préstamo

> Observación: en la implementación actual se añadió validación en frontend para evitar crear préstamos si el stock es 0; la lógica de negocio en `LoanService` puede ampliarse para manejar decrementar stock y lanzar excepción si no hay stock.

## Notas de implementación relevantes

- Se añadió la entidad `ArticuloBaja` y `ArticuloLiquidacion` en `servweb1_t2.Domain/Entities` y los `DbSet` correspondientes en `ApplicationDbContext`.
- El patrón Unit of Work está implementado en `servweb1_t2.Infrastructure/Persistence/Repositories/UnitOfWork.cs` y expone `BeginTransactionAsync`, `CommitTransactionAsync` y `RollbackTransactionAsync`.
- `BookService.DarBajaAsync(int id)` implementa el flujo solicitado: inicia transacción, inserta `ArticuloBaja`, inserta `ArticuloLiquidacion` si `Stock>0`, elimina el libro y confirma o revierte la transacción.

## Probar localmente (flujo DarBaja)

1. Arrancar la API (ver sección Ejecutar).
2. Usar `curl` o Postman:

    # listar libros
    curl http://localhost:5004/api/Book

    # dar de baja (ejemplo id=3)
    curl -X POST http://localhost:5004/api/Book/3/dar-baja

3. Comprobar tablas en MySQL:

    USE library_db;
    SELECT * FROM ArticulosBaja;
    SELECT * FROM ArticulosLiquidacion;

## Integración con frontend

Se preparó un frontend mínimo en `frontend/` (Vite + React). El `vite.config.js` contiene un proxy para redirigir `/api` a `http://localhost:5004` durante desarrollo.

