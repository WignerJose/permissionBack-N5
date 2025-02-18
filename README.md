Permission-N5

📌 Descripción

Permission-N5 es un proyecto desarrollado en .NET Core que implementa buenas prácticas de arquitectura de software, asegurando una estructura modular y escalable.
Se ha aplicado una separación de capas adecuada y se han incorporado patrones de diseño modernos para mejorar la mantenibilidad del código.

🚀 Tecnologías y Patrones Implementados

CQRS (Command Query Responsibility Segregation) para separar las operaciones de lectura y escritura.

Unit of Work para gestionar transacciones de manera eficiente.

ElasticSearch para la persistencia y búsqueda avanzada de permisos.

SQL Server para la gestión de permisos de manera tradicional.

Entity Framework para la conexión con SQL Server y el manejo de datos.

Patrón Repository para una abstracción clara en la capa de datos.

🛠️ Instalación y Configuración

Clona el repositorio:

git clone https://github.com/tu_usuario/Permission-N5.git

Configura la base de datos en SQL Server y actualiza la cadena de conexión en appsettings.json.

Ejecuta las migraciones de Entity Framework (si aplica):

dotnet ef database update

Ejecuta el proyecto:

dotnet run

📜 Contribuciones

Las contribuciones son bienvenidas. Para colaborar, sigue estos pasos:

Haz un fork del repositorio.

Crea una rama para tu función o corrección de errores.

Envía un pull request describiendo los cambios.

📝 Licencia

Este proyecto está bajo la licencia MIT.

💡 Nota: Si tienes dudas o sugerencias, no dudes en abrir un issue o contactar a los mantenedores del proyecto.
