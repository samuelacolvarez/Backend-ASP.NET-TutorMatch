# TutorMatch Backend

API REST para una plataforma de tutorías académicas. Permite registrar tutores, estudiantes y reservas de sesiones.

---

## Tecnologías usadas

- **ASP.NET Core 8** — framework principal
- **Entity Framework Core** — acceso a base de datos
- **SQL Server** — base de datos
- **ASP.NET Identity** — manejo de usuarios y roles
- **JWT (JSON Web Tokens)** — autenticación
- **FluentValidation** — validación de datos
- **Scalar / Swagger** — documentación de la API

---

## Requisitos para correr el proyecto

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server instalado localmente
- Visual Studio 2022 o VS Code

---

## Configuración

1. Clona el repositorio:
   ```bash
   git clone https://github.com/tu-usuario/TutorMatch_Backend.git
   cd TutorMatch_Backend
   ```

2. Abre `appsettings.json` y ajusta la cadena de conexión con el nombre de tu instancia de SQL Server:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\TU_INSTANCIA;Database=TutorMatchDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. Aplica las migraciones para crear la base de datos:
   ```bash
   dotnet ef database update
   ```

4. Corre el proyecto:
   ```bash
   dotnet run
   ```

---

## Documentación de la API

Con el proyecto corriendo, abre en el navegador:

```
http://localhost:{puerto}/scalar/v1
```

Ahí puedes ver y probar todos los endpoints disponibles.

---

## Estructura del proyecto

```
TutorMatch_Backend/
├── Controllers/       # Endpoints de la API
├── Services/          # Lógica de negocio
│   └── Interfaces/    # Contratos de los servicios
├── Models/            # Entidades de la base de datos
├── DTOs/              # Objetos de transferencia de datos
├── Validators/        # Validaciones con FluentValidation
├── Data/              # DbContext y configuración de EF
├── Migrations/        # Migraciones de la base de datos
└── Program.cs         # Configuración y arranque de la app
```

---

## Base de datos

El script para recrear la base de datos está disponible en el siguiente enlace:

🔗[https://eiaedu-my.sharepoint.com/:u:/g/personal/samuel_acosta28_eia_edu_co/IQBU6-KWVtotTbeWp3MZ0S76AVUYoVH9M54aM1JE09D3UlE?e=UQemCS ](https://eiaedu-my.sharepoint.com/:u:/g/personal/samuel_acosta28_eia_edu_co/IQBU6-KWVtotTbeWp3MZ0S76AVUYoVH9M54aM1JE09D3UlE?e=UQemCS)

---

## Integrantes del equipo

Ana Sofia Henao - Samuel Acosta 
