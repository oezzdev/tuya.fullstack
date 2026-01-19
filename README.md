# Tuya Fullstack — Instrucciones de la solución

Resumen
- Repositorio modular .NET 8 con módulos principales: `Cards`, `Users`, `Payments` y la API frontal en `src/Api`.
- La API central se encuentra en `src/Api` y agrega los módulos con configuración compartida (CORS, JWT, Swagger).

Arquitectura
- Estructura modular por dominio: cada módulo (por ejemplo `Cards`, `Users`, `Payments`) sigue una separación en capas o responsabilidades claras:
  - `*.Api` — capa de presentación/entrada (controladores, endpoints, configuración específica del módulo).
  - `*.Application` — lógica de aplicación (casos de uso, queries/commands, DTOs). Aquí se usa un patrón mediador/CQRS para desacoplar controladores de la lógica.
  - `*.Domain` y `*.Infrastructure` — dominio y persistencia si están presentes en el módulo.
- Las responsabilidades principales:
  - Configuración central en `src/Api/Program.cs` que registra middleware compartido (CORS, autenticación JWT, Swagger) y luego invoca las extensiones de configuración de cada módulo (`ConfigureCardsModule`, `ConfigureUsersModule`, `ConfigurePaymentsModule`).
  - Cada módulo expone métodos de extensión para registrar servicios y middleware necesarios; la composición final ocurre en `Program.cs`.
- Comunicación interna:
  - Se utiliza un mediador para enviar `Commands` y `Queries` hacia los handlers en `*.Application`.
  - Los controladores solo orquestan entrada/validación mínima y delegan el trabajo al mediador.
- Persistencia:
  - Configuración por módulo en `src/Api/appsettings.json` con cadenas de conexión independientes (ej. `ConnectionStrings:Cards`, `ConnectionStrings:Users`, `ConnectionStrings:Payments`). En desarrollo se usan archivos SQLite locales en `./Data`.
  - Esto permite aislar datos por módulo durante el desarrollo y pruebas locales.
- Seguridad y operaciones:
  - Autenticación JWT con clave simétrica configurada en `SignatureOptions:SymmetricKey`.
  - Documentación OpenAPI/Swagger habilitada en entorno `Development`.
- Ventajas de la arquitectura:
  - Alta cohesión por módulo y bajo acoplamiento entre dominios.
  - Fácil reemplazo o extensión de un módulo sin tocar la composición global.
  - Escalabilidad: componentes pueden moverse a microservicios si fuera necesario.
- Cómo añadir un nuevo módulo (resumen):
  - Crear la estructura `NewDomain.Api`, `NewDomain.Application`, `NewDomain.Domain` y `NewDomain.Infrastructure`.
  - Exponer métodos `ConfigureNewDomainModule` y `UseNewDomainModule`.
  - Registrar ensamblados en el mediador y llamar a las extensiones desde `src/Api/Program.cs`.

Requisitos previos
- .NET 8 SDK instalado.
- Visual Studio 2026 (o VS Code / CLI si prefieres).
- (Opcional) Herramientas SQLite para inspeccionar archivos `.db`.

Abrir la solución
- Abre la carpeta raíz del repositorio en Visual Studio 2026 o abre el archivo de solución si existe.
- Desde el Explorador de soluciones, establece el proyecto `src/Api` como proyecto de inicio: botón derecho sobre el proyecto -> __Set as StartUp Project__.

Configuración importante
- Archivo de configuración principal: `src/Api/appsettings.json`.
  - Las cadenas de conexión por defecto usan SQLite y apuntan a `Data` relativo al directorio de trabajo:
    - `ConnectionStrings:Cards` → `Data Source=./Data/cards.db`
    - `ConnectionStrings:Users` → `Data Source=./Data/users.db`
    - `ConnectionStrings:Payments` → `Data Source=./Data/payments.db`
  - Clave simétrica para JWT en `SignatureOptions:SymmetricKey` (valor por defecto presente para desarrollo). Cámbiala en producción.

Preparar el entorno
- Crea la carpeta `Data` en el directorio desde donde se ejecuta la API si no existe:
  - En Windows PowerShell: `mkdir src\Api\Data`
  - En CMD: `md src\Api\Data`
- Asegúrate de permisos de escritura en esa carpeta para que los archivos SQLite puedan crearse.

Ejecutar la API (Visual Studio)
- Selecciona `src/Api` como proyecto de inicio (ver sección anterior).
- Ejecuta la aplicación en __Debug__ o __Start Debugging__ (F5) o __Start Without Debugging__ (Ctrl+F5).
- En entorno `Development`, Swagger UI estará disponible bajo `/docs` (p.ej. `https://localhost:{puerto}/docs`).

Ejecutar desde CLI
- Desde la raíz del repo:
  - `cd src\Api`
  - `dotnet run`
- Observa en la consola la URL y puerto asignados; Swagger estará disponible en `/docs` si el entorno es `Development`.

Endpoints y uso rápido
- Documentación OpenAPI/Swagger: `/docs` (habilitado en Development).
- CORS: los orígenes permitidos se leen desde `AllowedHosts` en `appsettings.json`.
- Autenticación: JWT con esquema Bearer; la validación usa `SignatureOptions:SymmetricKey`.

Recomendaciones para desarrollo
- Mantén `ASPNETCORE_ENVIRONMENT=Development` mientras desarrollas para ver Swagger y mensajes más verbosos.
- Si cambias `SignatureOptions:SymmetricKey`, asegúrate de regenerar/emitir tokens compatibles para pruebas.
- Para reiniciar datos locales: detén la app y elimina los archivos `.db` dentro de `src\Api\Data`.

Problemas comunes
- Si no ves Swagger:
  - Verifica que `ASPNETCORE_ENVIRONMENT` esté en `Development`.
  - Revisa que el proyecto `src/Api` se esté ejecutando y que la ruta `/docs` no esté bloqueada por proxy/CORS.
- Errores de escritura en la DB:
  - Verifica permisos de la carpeta `src\Api\Data`.
  - Asegúrate de que no haya procesos bloqueando los archivos `.db`.

Archivos clave
- `src/Api/Program.cs` — punto de entrada y configuración de módulos.
- `src/Api/appsettings.json` — configuración de conexión, CORS y JWT.
- Módulos:
  - `src/Modules/Cards/*`
  - `src/Modules/Users/*`
  - `src/Modules/Payments/*`
