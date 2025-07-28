Descripción
Aplicación modular para la gestión integral de un motel, desarrollada en .NET Core. El sistema está organizado en cuatro capas para facilitar el mantenimiento, la escalabilidad y la separación de responsabilidades:

Capa Web: APIs RESTful desarrolladas con ASP.NET Core que exponen los servicios para manipular datos y lógica de negocio.

Capa Core: Contiene la lógica de negocio, validaciones y modelos de dominio.

Capa de Integración: Gestiona la comunicación con la base de datos SQL Server, incluyendo procedimientos almacenados y operaciones CRUD.

Capa de Caja: Interfaz gráfica basada en Windows Forms que consume las APIs para interacción directa con el usuario, permitiendo mantenimiento y gestión del motel.

Funcionalidades principales
Administración completa de habitaciones, clientes, reservas y servicios adicionales.

APIs bien definidas para comunicación entre la capa web y la capa de caja.

Uso de procedimientos almacenados para optimizar operaciones en la base de datos.

Control de versiones con Git y repositorio en GitHub.
