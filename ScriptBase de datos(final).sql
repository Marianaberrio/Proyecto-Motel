USE [master]
GO
/****** Object:  Database [dbMotel]    Script Date: 10/7/2025 10:06:07 p. m. ******/
CREATE DATABASE [dbMotel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbMotel', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\dbMotel.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbMotel_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\dbMotel_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [dbMotel] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbMotel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbMotel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbMotel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbMotel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbMotel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbMotel] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbMotel] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [dbMotel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbMotel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbMotel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbMotel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbMotel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbMotel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbMotel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbMotel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbMotel] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dbMotel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbMotel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbMotel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbMotel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbMotel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbMotel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbMotel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbMotel] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbMotel] SET  MULTI_USER 
GO
ALTER DATABASE [dbMotel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbMotel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbMotel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbMotel] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbMotel] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbMotel] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [dbMotel] SET QUERY_STORE = ON
GO
ALTER DATABASE [dbMotel] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [dbMotel]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 10/7/2025 10:06:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[NumCliente] [int] IDENTITY(1,1) NOT NULL,
	[NombreCliente] [varchar](100) NOT NULL,
	[ApellidoCliente] [varchar](100) NOT NULL,
	[CorreoCliente] [varchar](150) NOT NULL,
	[TelefonoCliente] [char](10) NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[FechaNacimiento] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NumCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Habitaciones]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Habitaciones](
	[IdHabitacion] [int] IDENTITY(1,1) NOT NULL,
	[NumHabitacion] [varchar](10) NOT NULL,
	[TipoHabitacion] [varchar](50) NOT NULL,
	[PrecioHabitacion] [decimal](18, 2) NOT NULL,
	[EstadoHabitacion] [varchar](50) NOT NULL,
	[CapacidadHabitacion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHabitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_NumHabitacion] UNIQUE NONCLUSTERED 
(
	[NumHabitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagos]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[NumPago] [int] IDENTITY(1,1) NOT NULL,
	[NumReserva] [int] NOT NULL,
	[MontoPago] [decimal](18, 2) NOT NULL,
	[FechaPago] [datetime] NOT NULL,
	[MetodoPago] [varchar](50) NOT NULL,
	[EstadoPago] [varchar](50) NOT NULL,
	[ComentarioPago] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[NumPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservaHabitacion]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservaHabitacion](
	[NumReserva] [int] NOT NULL,
	[IdHabitacion] [int] NOT NULL,
	[PrecioHabitacion] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NumReserva] ASC,
	[IdHabitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservas]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservas](
	[NumReserva] [int] IDENTITY(1,1) NOT NULL,
	[NumCliente] [int] NOT NULL,
	[FechaReserva] [datetime] NOT NULL,
	[FechaEntrada] [datetime] NOT NULL,
	[FechaSalida] [datetime] NOT NULL,
	[EstadoReserva] [varchar](50) NOT NULL,
	[TotalReserva] [decimal](18, 2) NOT NULL,
	[ComentarioReserva] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[NumReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservaServicios]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservaServicios](
	[NumReserva] [int] NOT NULL,
	[NumServicio] [int] NOT NULL,
	[PrecioServicio] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NumReserva] ASC,
	[NumServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicios]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicios](
	[NumServicio] [int] IDENTITY(1,1) NOT NULL,
	[NombreServicio] [varchar](100) NOT NULL,
	[DescripcionServicio] [varchar](255) NULL,
	[PrecioServicio] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NumServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](30) NOT NULL,
	[Contraseña] [varchar](50) NOT NULL,
	[Rol] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Pagos] ADD  DEFAULT (getdate()) FOR [FechaPago]
GO
ALTER TABLE [dbo].[Reservas] ADD  DEFAULT (getdate()) FOR [FechaReserva]
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD FOREIGN KEY([NumReserva])
REFERENCES [dbo].[Reservas] ([NumReserva])
GO
ALTER TABLE [dbo].[ReservaHabitacion]  WITH CHECK ADD FOREIGN KEY([IdHabitacion])
REFERENCES [dbo].[Habitaciones] ([IdHabitacion])
GO
ALTER TABLE [dbo].[ReservaHabitacion]  WITH CHECK ADD FOREIGN KEY([NumReserva])
REFERENCES [dbo].[Reservas] ([NumReserva])
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD FOREIGN KEY([NumCliente])
REFERENCES [dbo].[Clientes] ([NumCliente])
GO
ALTER TABLE [dbo].[ReservaServicios]  WITH CHECK ADD FOREIGN KEY([NumReserva])
REFERENCES [dbo].[Reservas] ([NumReserva])
GO
ALTER TABLE [dbo].[ReservaServicios]  WITH CHECK ADD FOREIGN KEY([NumServicio])
REFERENCES [dbo].[Servicios] ([NumServicio])
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCliente]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarCliente]
    @NumCliente INT,
    @NombreCliente VARCHAR(100),
    @ApellidoCliente VARCHAR(100),
    @CorreoCliente VARCHAR(150),
    @TelefonoCliente CHAR(10),
    @FechaNacimiento DATETIME
AS
BEGIN
    UPDATE [dbo].[Clientes]
    SET 
        [NombreCliente] = @NombreCliente,
        [ApellidoCliente] = @ApellidoCliente,
        [CorreoCliente] = @CorreoCliente,
        [TelefonoCliente] = @TelefonoCliente,
        [FechaNacimiento] = @FechaNacimiento
    WHERE [NumCliente] = @NumCliente
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoPago]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEstadoPago]
    @NumPago INT,
    @EstadoPago VARCHAR(50)
AS
BEGIN
    -- Actualizar solo el estado del pago
    UPDATE [dbo].[Pagos]
    SET EstadoPago = @EstadoPago
    WHERE NumPago = @NumPago;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoReserva]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEstadoReserva]
    @NumReserva INT,
    @EstadoReserva VARCHAR(50)
AS
BEGIN
    UPDATE [dbo].[Reservas]
    SET [EstadoReserva] = @EstadoReserva
    WHERE [NumReserva] = @NumReserva
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarHabitacion]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarHabitacion]
    @IdHabitacion INT = NULL,
    @NumHabitacion VARCHAR(10) = NULL,
    @TipoHabitacion VARCHAR(50),
    @PrecioHabitacion DECIMAL(18, 2),
    @EstadoHabitacion VARCHAR(50),
    @CapacidadHabitacion INT
AS
BEGIN
    -- Usamos la condición OR para permitir que se actualice con cualquiera de los dos parámetros
    UPDATE [dbo].[Habitaciones]
    SET 
        [NumHabitacion] = @NumHabitacion,
        [TipoHabitacion] = @TipoHabitacion,
        [PrecioHabitacion] = @PrecioHabitacion,
        [EstadoHabitacion] = @EstadoHabitacion,
        [CapacidadHabitacion] = @CapacidadHabitacion
    WHERE 
        (@IdHabitacion IS NOT NULL AND [IdHabitacion] = @IdHabitacion)  -- Si se pasa IdHabitacion
        OR
        (@NumHabitacion IS NOT NULL AND [NumHabitacion] = @NumHabitacion)  -- Si se pasa NumHabitacion
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarReserva]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarReserva]
    @NumReserva INT,
    @FechaEntrada DATETIME,
    @FechaSalida DATETIME,
    @EstadoReserva VARCHAR(50),
    @TotalReserva DECIMAL(18, 2),
    @ComentarioReserva VARCHAR(255)
AS
BEGIN
    -- Actualizar solo los datos de la reserva
    UPDATE [dbo].[Reservas]
    SET 
        [FechaEntrada] = @FechaEntrada,
        [FechaSalida] = @FechaSalida,
        [EstadoReserva] = @EstadoReserva,
        [TotalReserva] = @TotalReserva,
        [ComentarioReserva] = @ComentarioReserva
    WHERE 
        [NumReserva] = @NumReserva;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarServicio]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarServicio]
    @NumServicio INT,
    @NombreServicio VARCHAR(100),
    @DescripcionServicio VARCHAR(255),
    @PrecioServicio DECIMAL(18, 2)
AS
BEGIN
    UPDATE [dbo].[Servicios]
    SET 
        [NombreServicio] = @NombreServicio,
        [DescripcionServicio] = @DescripcionServicio,
        [PrecioServicio] = @PrecioServicio
    WHERE [NumServicio] = @NumServicio
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarUsuario]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarUsuario]
    @Usuario VARCHAR(30),
    @Contraseña VARCHAR(50),
    @Rol VARCHAR(15)
AS
BEGIN
    INSERT INTO [dbo].[Usuarios] ([Usuario], [Contraseña], [Rol])
    VALUES (@Usuario, @Contraseña, @Rol);
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarClientePorEmail]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarClientePorEmail]
    @CorreoCliente VARCHAR(255)
AS
BEGIN
    -- Consultamos el ID (NumCliente) de un cliente con el correo especificado
    SELECT NumCliente
    FROM Clientes
    WHERE CorreoCliente = @CorreoCliente;
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarUsuario]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarUsuario]
    @Id INT = NULL,
    @Usuario VARCHAR(30) = NULL
AS
BEGIN
    SELECT [Id], [Usuario], [Contraseña], [Rol]
    FROM [dbo].[Usuarios]
    WHERE (@Id IS NULL OR [Id] = @Id)
    AND (@Usuario IS NULL OR [Usuario] = @Usuario);
END
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoPago]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CambiarEstadoPago]
    @NumPago INT,
    @EstadoPago VARCHAR(50)   -- El nuevo estado del pago (ej. 'Pagado', 'Pendiente', etc.)
AS
BEGIN
    -- Actualiza solo el estado del pago
    UPDATE [dbo].[Pagos]
    SET EstadoPago = @EstadoPago
    WHERE NumPago = @NumPago;
END
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoReserva]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CambiarEstadoReserva]
    @NumReserva INT,
    @EstadoReserva VARCHAR(50)   -- El nuevo estado de la reserva (ej. 'Cancelado', 'Completada', etc.)
AS
BEGIN
    -- Solo se actualiza el estado de la reserva
    UPDATE [dbo].[Reservas]
    SET EstadoReserva = @EstadoReserva
    WHERE NumReserva = @NumReserva;
END
GO
/****** Object:  StoredProcedure [dbo].[CancelarReserva]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CancelarReserva]
    @NumReserva INT
AS
BEGIN
    -- Actualizar el estado de la reserva a 'Cancelada'
    UPDATE [dbo].[Reservas]
    SET [EstadoReserva] = 'Cancelada'
    WHERE [NumReserva] = @NumReserva

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarCliente]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarCliente]
    @NumCliente INT
AS
BEGIN
    SELECT * FROM [dbo].[Clientes]
    WHERE [NumCliente] = @NumCliente
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarHabitacion]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarHabitacion]
    @NumHabitacion VARCHAR(10) = NULL,   -- Parámetro para buscar por número de habitación
    @IdHabitacion INT = NULL              -- Parámetro para buscar por ID de habitación
AS
BEGIN
    -- Realizamos la búsqueda usando un OR para comprobar ambos parámetros
    SELECT * FROM [dbo].[Habitaciones]
    WHERE (@NumHabitacion IS NULL OR [NumHabitacion] = @NumHabitacion)
    OR (@IdHabitacion IS NULL OR [IdHabitacion] = @IdHabitacion)
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarPago]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarPago]
    @NumPago INT = NULL,     -- Puede ser null si se quiere buscar por NumReserva
    @NumReserva INT = NULL   -- Puede ser null si se quiere buscar por NumPago
AS
BEGIN
    -- Si se pasa NumPago, se consulta por NumPago
    IF @NumPago IS NOT NULL
    BEGIN
        SELECT * 
        FROM [dbo].[Pagos]
        WHERE [NumPago] = @NumPago;
    END
    -- Si se pasa NumReserva, se consulta por NumReserva
    ELSE IF @NumReserva IS NOT NULL
    BEGIN
        SELECT * 
        FROM [dbo].[Pagos]
        WHERE [NumReserva] = @NumReserva;
    END
    -- Si no se pasa ni NumPago ni NumReserva, no se hace nada
    ELSE
    BEGIN
        SELECT 'Debe proporcionar al menos un valor para la búsqueda (NumPago o NumReserva)';
    END
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarReserva]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarReserva]
    @NumReserva INT
AS
BEGIN
    SELECT 
        r.NumReserva,
        r.NumCliente,
        r.FechaReserva,
        r.FechaEntrada,
        r.FechaSalida,
        r.EstadoReserva,
        r.TotalReserva,
        r.ComentarioReserva
    FROM 
        [dbo].[Reservas] r
    WHERE 
        r.NumReserva = @NumReserva;
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarReservasPorCliente]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarReservasPorCliente]
    @NumCliente INT
AS
BEGIN
    SELECT * FROM [dbo].[Reservas]
    WHERE [NumCliente] = @NumCliente
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarServicio]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarServicio]
    @NumServicio INT
AS
BEGIN
    SELECT * FROM [dbo].[Servicios]
    WHERE [NumServicio] = @NumServicio
END
GO
/****** Object:  StoredProcedure [dbo].[CrearCliente]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearCliente]
    @NombreCliente VARCHAR(100),
    @ApellidoCliente VARCHAR(100),
    @CorreoCliente VARCHAR(150),
    @TelefonoCliente CHAR(10),
    @FechaNacimiento DATETIME
AS
BEGIN
    INSERT INTO [dbo].[Clientes]
    ([NombreCliente], [ApellidoCliente], [CorreoCliente], [TelefonoCliente], [FechaNacimiento])
    VALUES
    (@NombreCliente, @ApellidoCliente, @CorreoCliente, @TelefonoCliente, @FechaNacimiento)
END
GO
/****** Object:  StoredProcedure [dbo].[CrearHabitacion]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearHabitacion]
    @NumHabitacion VARCHAR(10),
    @TipoHabitacion VARCHAR(50),
    @PrecioHabitacion DECIMAL(18, 2),
    @EstadoHabitacion VARCHAR(50),
    @CapacidadHabitacion INT
AS
BEGIN
    INSERT INTO [dbo].[Habitaciones]
    ([NumHabitacion], [TipoHabitacion], [PrecioHabitacion], [EstadoHabitacion], [CapacidadHabitacion])
    VALUES
    (@NumHabitacion, @TipoHabitacion, @PrecioHabitacion, @EstadoHabitacion, @CapacidadHabitacion)
END
GO
/****** Object:  StoredProcedure [dbo].[CrearReserva]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearReserva]
    @NumCliente INT,
    @FechaEntrada DATETIME,
    @FechaSalida DATETIME,
    @EstadoReserva VARCHAR(50),
    @TotalReserva DECIMAL(18, 2),
    @ComentarioReserva VARCHAR(255)
AS
BEGIN
    -- Crear la reserva sin asociar la habitación
    INSERT INTO [dbo].[Reservas] 
    ([NumCliente], [FechaReserva], [FechaEntrada], [FechaSalida], [EstadoReserva], [TotalReserva], [ComentarioReserva])
    VALUES 
    (@NumCliente, GETDATE(), @FechaEntrada, @FechaSalida, @EstadoReserva, @TotalReserva, @ComentarioReserva);
END
GO
/****** Object:  StoredProcedure [dbo].[CrearServicio]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearServicio]
    @NombreServicio VARCHAR(100),
    @DescripcionServicio VARCHAR(255),
    @PrecioServicio DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO [dbo].[Servicios]
    ([NombreServicio], [DescripcionServicio], [PrecioServicio])
    VALUES
    (@NombreServicio, @DescripcionServicio, @PrecioServicio)
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarCliente]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarCliente]
    @NumCliente INT
AS
BEGIN
    DELETE FROM [dbo].[Clientes]
    WHERE [NumCliente] = @NumCliente
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarHabitacion]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarHabitacion]
    @numHabitacion INT
AS
BEGIN
    DELETE FROM [dbo].[Habitaciones]
    WHERE [numHabitacion] = @numHabitacion
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarServicio]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarServicio]
    @NumServicio INT
AS
BEGIN
    DELETE FROM [dbo].[Servicios]
    WHERE [NumServicio] = @NumServicio
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarUsuario]
    @Id INT = NULL,
    @Usuario VARCHAR(30) = NULL
AS
BEGIN
    DELETE FROM [dbo].[Usuarios]
    WHERE (@Id IS NULL OR [Id] = @Id)
    AND (@Usuario IS NULL OR [Usuario] = @Usuario);
END
GO
/****** Object:  StoredProcedure [dbo].[ModificarUsuario]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModificarUsuario]
    @Id INT = NULL,                     -- Parámetro para buscar por ID de usuario
    @Usuario VARCHAR(30) = NULL,        -- Parámetro para buscar por nombre de usuario
    @Contraseña VARCHAR(50),            -- Nueva contraseña
    @Rol VARCHAR(15)                    -- Nuevo rol
AS
BEGIN
    UPDATE [dbo].[Usuarios]
    SET 
        [Contraseña] = @Contraseña,     -- Actualizamos la contraseña
        [Rol] = @Rol                    -- Actualizamos el rol
    WHERE (@Id IS NULL OR [Id] = @Id)    -- Si se pasa el ID, lo usamos
    AND (@Usuario IS NULL OR [Usuario] = @Usuario)  -- Si se pasa el nombre de usuario, lo usamos
END
GO
/****** Object:  StoredProcedure [dbo].[RealizarPago]    Script Date: 10/7/2025 10:06:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RealizarPago]
    @NumReserva INT,
    @MontoPago DECIMAL,
    @MetodoPago VARCHAR(50),
    @EstadoPago VARCHAR(50)
AS
BEGIN
    -- Insertar el pago en la tabla Pagos
    INSERT INTO [dbo].[Pagos] 
    ([NumReserva], [FechaPago], [MontoPago], [MetodoPago], [EstadoPago])
    VALUES 
    (@NumReserva, GETDATE(), @MontoPago, @MetodoPago, @EstadoPago)
END
GO
USE [master]
GO
ALTER DATABASE [dbMotel] SET  READ_WRITE 
GO
