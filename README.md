# 📚 Library Management API

API RESTful para la gestión de préstamos de libros, usuarios y ejemplares.

---

## ⚙️ Tecnologías

- C#
- .NET
- Entity Framework
- SQL Server
- JWT (HMAC256)
- Identity
- CQRS + Mediator
- Onion Architecture
- FluentValidation
- Ardalis.Specification
- Swagger
- Transacciones DB
- (Próximamente: Serilog)

---

## 🔐 Autenticación

- Registro/Login con JWT (Bearer Token)
- Seguridad implementada con algoritmo HMAC256
- Swagger con autenticación habilitada

---

## 📦 Funcionalidades

- CRUD para:
  - Usuarios
  - Libros
  - Préstamos
- Paginación con Ardalis.Specification
- Transacciones para integridad en base de datos

---

## ▶️ Ejecutar

1. Clona el repo
2. Configura tu cadena de conexión en `appsettings.json`
3. Ejecuta `Update-Database` para aplicar migraciones
4. Corre el proyecto en Visual Studio
5. Abre Swagger en `https://localhost:<puerto>/swagger`

---

---

## 🚧 En progreso

- Logging con Serilog
- Validaciones adicionales
- Mejoras en la documentación de Swagger
- Optimización de consultas
- Implementación de pruebas unitarias
- Implementación de pruebas de integración
- Redis para caché


📌 Notas
- Este proyecto es un ejemplo de cómo implementar una API RESTful para la gestión de préstamos de libros, usuarios y ejemplares. Está diseñado para ser escalable y fácil de mantener, siguiendo las mejores prácticas de desarrollo de software.

-Tambien me permite mantener consistencia en mi area de trabajo, ya que estoy utilizando las mismas herramientas y patrones de diseño que en mis proyectos actuales.

-Manteniendo y expandiendo mi conocimiento en C# y .NET, lo que me permite ser más eficiente y productivo en mi camino para llegar a ser buen programador en un futuro cercano.
