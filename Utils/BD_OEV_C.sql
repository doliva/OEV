USE [master]
GO

/****** Object:  Database [BD_OEV_C]    Script Date: 08/12/2017 09:23:31 p.m. ******/
DROP DATABASE [BD_OEV_C]
GO

/****** Object:  Database [BD_OEV_C]    Script Date: 08/12/2017 09:23:31 p.m. ******/
CREATE DATABASE [BD_OEV_C]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_OEV_C', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\BD_OEV_C.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BD_OEV_C_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\BD_OEV_C_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [BD_OEV_C] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BD_OEV_C].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [BD_OEV_C] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [BD_OEV_C] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [BD_OEV_C] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [BD_OEV_C] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [BD_OEV_C] SET ARITHABORT OFF 
GO

ALTER DATABASE [BD_OEV_C] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [BD_OEV_C] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [BD_OEV_C] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [BD_OEV_C] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [BD_OEV_C] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [BD_OEV_C] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [BD_OEV_C] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [BD_OEV_C] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [BD_OEV_C] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [BD_OEV_C] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [BD_OEV_C] SET  DISABLE_BROKER 
GO

ALTER DATABASE [BD_OEV_C] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [BD_OEV_C] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [BD_OEV_C] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [BD_OEV_C] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [BD_OEV_C] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [BD_OEV_C] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [BD_OEV_C] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [BD_OEV_C] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [BD_OEV_C] SET  MULTI_USER 
GO

ALTER DATABASE [BD_OEV_C] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [BD_OEV_C] SET DB_CHAINING OFF 
GO

ALTER DATABASE [BD_OEV_C] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [BD_OEV_C] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [BD_OEV_C] SET  READ_WRITE 
GO

USE [BD_OEV_C]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Voucher_Producto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Voucher]'))
ALTER TABLE [dbo].[Voucher] DROP CONSTRAINT [FK_Voucher_Producto]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Voucher]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta] DROP CONSTRAINT [FK_Venta_Voucher]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Reserva]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta] DROP CONSTRAINT [FK_Venta_Reserva]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta] DROP CONSTRAINT [FK_Venta_Factura]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Cliente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta] DROP CONSTRAINT [FK_Venta_Cliente]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reserva_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reserva]'))
ALTER TABLE [dbo].[Reserva] DROP CONSTRAINT [FK_Reserva_Calendario]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Especialidad_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]'))
ALTER TABLE [dbo].[Instructor_Especialidad] DROP CONSTRAINT [FK_Instructor_Especialidad_Instructor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Especialidad_Especialidad]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]'))
ALTER TABLE [dbo].[Instructor_Especialidad] DROP CONSTRAINT [FK_Instructor_Especialidad_Especialidad]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Detalle_Factura_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura] DROP CONSTRAINT [FK_Detalle_Factura_Factura]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Voucher]') AND type in (N'U'))
DROP TABLE [dbo].[Voucher]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Venta]') AND type in (N'U'))
DROP TABLE [dbo].[Venta]
GO
/****** Object:  Table [dbo].[Traslado]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Traslado]') AND type in (N'U'))
DROP TABLE [dbo].[Traslado]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reserva]') AND type in (N'U'))
DROP TABLE [dbo].[Reserva]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Producto]') AND type in (N'U'))
DROP TABLE [dbo].[Producto]
GO
/****** Object:  Table [dbo].[Instructor_Especialidad]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor_Especialidad]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor]
GO
/****** Object:  Table [dbo].[Horario]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Horario]') AND type in (N'U'))
DROP TABLE [dbo].[Horario]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Factura]') AND type in (N'U'))
DROP TABLE [dbo].[Factura]
GO
/****** Object:  Table [dbo].[Especialidad]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Especialidad]') AND type in (N'U'))
DROP TABLE [dbo].[Especialidad]
GO
/****** Object:  Table [dbo].[Detalle_Factura]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]') AND type in (N'U'))
DROP TABLE [dbo].[Detalle_Factura]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type in (N'U'))
DROP TABLE [dbo].[Cliente]
GO
/****** Object:  Table [dbo].[Calendario]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario]') AND type in (N'U'))
DROP TABLE [dbo].[Calendario]
GO
/****** Object:  Table [dbo].[Alojamiento]    Script Date: 08/12/2017 08:25:22 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Alojamiento]') AND type in (N'U'))
DROP TABLE [dbo].[Alojamiento]
GO
/****** Object:  Table [dbo].[Alojamiento]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Alojamiento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Alojamiento](
	[id_alojamiento] [int] IDENTITY(1,1) NOT NULL,
	[razon_social] [varchar](50) NULL,
	[cuit] [varchar](15) NOT NULL,
	[direccion] [varchar](100) NULL,
	[ciudad] [varchar](50) NULL,
	[telefono] [varchar](20) NULL,
	[tarifa] [money] NULL,
	[categoria] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[capacidad] [int] NULL,
	[servicios] [varchar](2000) NULL,
 CONSTRAINT [PK_Alojamiento] PRIMARY KEY CLUSTERED 
(
	[id_alojamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Calendario]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Calendario](
	[id_calendario] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [int] NOT NULL,
	[id_alojamiento] [int] NOT NULL,
	[legajo] [int] NOT NULL,
	[id_traslado] [int] NOT NULL,
	[anio] [int] NULL,
	[cupo] [int] NULL,
	[fecha_salida] [datetime] NULL,
 CONSTRAINT [PK_Calendario] PRIMARY KEY CLUSTERED 
(
	[id_calendario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cliente](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[apellido] [varchar](30) NULL,
	[nombre] [varchar](30) NULL,
	[dni] [varchar](10) NULL,
	[pasaporte] [varchar](10) NULL,
	[domicilio] [varchar](100) NULL,
	[ciudad] [varchar](50) NULL,
	[celular] [varchar](20) NULL,
	[email] [varchar](30) NULL,
	[estado] [bit] NOT NULL,
	[telefono] [varchar](20) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Detalle_Factura]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Detalle_Factura](
	[id_detalle] [int] IDENTITY(1,1) NOT NULL,
	[id_factura] [int] NOT NULL,
	[detalle] [varchar](50) NULL,
	[precio] [money] NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [PK_Detalle_Factura] PRIMARY KEY CLUSTERED 
(
	[id_detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Especialidad]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Especialidad]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Especialidad](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Especialidad] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Factura]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Factura](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NOT NULL,
	[estado] [varchar](50) NULL,
	[importe] [money] NULL,
	[tipo_pago] [varchar](50) NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Horario]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Horario]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Horario](
	[nombre] [varchar](50) NOT NULL,
	[mes] [varchar](15) NOT NULL,
	[dias] [varchar](10) NOT NULL,
	[hora_inicio] [time](0) NOT NULL,
	[hora_fin] [time](0) NOT NULL,
 CONSTRAINT [PK_Horario] PRIMARY KEY CLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instructor](
	[legajo] [int] IDENTITY(1,1) NOT NULL,
	[dni] [varchar](10) NOT NULL,
	[nombre] [varchar](30) NULL,
	[apellido] [varchar](30) NULL,
	[domicilio] [varchar](100) NULL,
	[ciudad] [varchar](50) NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](30) NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Instructor] PRIMARY KEY CLUSTERED 
(
	[legajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Instructor_Especialidad]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instructor_Especialidad](
	[legajo] [int] NOT NULL,
	[codigo] [int] NOT NULL,
	[tarifa] [money] NULL,
	[experiencia] [int] NULL,
 CONSTRAINT [PK_Instructor_Especialidad] PRIMARY KEY CLUSTERED 
(
	[legajo] ASC,
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Producto]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Producto](
	[id_producto] [int] IDENTITY(1,1) NOT NULL,
	[destino] [varchar](100) NULL,
	[actividad] [varchar](100) NULL,
	[estado] [bit] NULL,
	[nombre] [varchar](50) NOT NULL,
	[precio] [money] NULL,
	[duracion] [int] NULL,
	[tipo_producto] [varchar](20) NULL,
	[dificultad] [varchar](20) NULL,
	[descripcion] [varchar](8000) NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reserva]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Reserva](
	[codigo] [varchar](6) NOT NULL,
	[cantidad] [int] NULL,
	[fecha] [date] NOT NULL,
	[estado] [varchar](15) NOT NULL,
	[id_calendario] [int] NULL,
	[importe] [money] NULL,
	[tipo_pago] [varchar](15) NULL,
 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Traslado]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Traslado]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Traslado](
	[id_traslado] [int] IDENTITY(1,1) NOT NULL,
	[razon_social] [varchar](50) NULL,
	[cuit] [varchar](15) NOT NULL,
	[direccion] [varchar](100) NULL,
	[ciudad] [varchar](50) NULL,
	[tipo] [varchar](20) NULL,
	[tarifa] [money] NULL,
	[email] [varchar](50) NULL,
	[capacidad] [int] NULL,
	[telefono] [varchar](20) NULL,
 CONSTRAINT [PK_Traslado] PRIMARY KEY CLUSTERED 
(
	[id_traslado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Venta]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Venta](
	[id_cliente] [int] NOT NULL,
	[codigo] [varchar](6) NOT NULL,
	[id_factura] [int] NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC,
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 08/12/2017 08:25:22 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Voucher]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Voucher](
	[codigo] [varchar](6) NOT NULL,
	[id_producto] [int] NOT NULL,
	[fecha] [date] NULL,
	[estado] [varchar](15) NULL,
	[detalle] [varchar](50) NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Alojamiento] ON 

GO
INSERT [dbo].[Alojamiento] ([id_alojamiento], [razon_social], [cuit], [direccion], [ciudad], [telefono], [tarifa], [categoria], [email], [capacidad], [servicios]) VALUES (1, N'Punto Urbano', N'20-32904280-5', N'Av. Godoy Cruz 362', N'Mendoza', N'54 0261 429-528', 1000.0000, N'HOSTERIA', N'reservas@buscandoamerica.com.ar', 4, N'Desayuno, internet, terraza, lavandería, seguridad 24hs con cámaras.')
GO
SET IDENTITY_INSERT [dbo].[Alojamiento] OFF
GO
SET IDENTITY_INSERT [dbo].[Especialidad] ON 

GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (1, N'Instructor de escalada en roca')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (2, N'Instructor de escalada en hiel')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (3, N'Guia profesional')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (4, N'Profesor de Educación Física')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (5, N'Socorrista')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (6, N'Guía profesional de trekking en cordillera')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (7, N'Instructor de andinismo')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (8, N'Instructor de Primeros Socorros')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (9, N'Instructor de RCP')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (10, N'Guía de Cabalgatas')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (11, N'Trekking')
GO
SET IDENTITY_INSERT [dbo].[Especialidad] OFF
GO
INSERT [dbo].[Horario] ([nombre], [mes], [dias], [hora_inicio], [hora_fin]) VALUES (N'GPS_FAC_L_0900:1800', N'',N'L', CAST(0x00907E0000000000 AS Time), CAST(0x0020FD0000000000 AS Time))
GO
INSERT [dbo].[Horario] ([nombre], [mes], [dias], [hora_inicio], [hora_fin]) VALUES (N'MONT_MED_LXV_1400:1600', N'', N'LXV', CAST(0x00E0C40000000000 AS Time), CAST(0x0000E10000000000 AS Time))
GO
SET IDENTITY_INSERT [dbo].[Instructor] ON 

GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (1, N'29356231', N'Leandro', N'Robledo', N'Amenabar 2314, Buenos Aires', N'Buenos Aires', N'11 15 48769023', N'leandro.robledo@gmail.com', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (2, N'31769256', N'Fernando', N'Scheuler', N'Ciudad de la Paz 2189, Buenos Aires', N'Buenos Aires', N'11 15 48973672', N'fernando.scheuler@gmail.com', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (3, N'33806835', N'David', N'Rivelli', N'Av Cabildo 3546, Buenos Aires', N'Buenos Aires', N'11 15 67543879', N'david.rivelli@gmail.com', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (4, N'28593275', N'Ivan', N'Calleri', N'Cuba 1845, Buenos Aires', N'Buenos Aires', N'11 15 54386054', N'ivan.calleri@gmail.com', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (5, N'32596235', N'German', N'Olivares', N'Moldes 1245, Buenos Aires', N'Buenos Aires', N'11 15 59506724', N'german.olivares@gmail.com', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (6, N'29356488', N'Juan', N'Martinez', N'Av. Roca 342', N'Florida Oeste, Vicente Lopez', N'4888999', N'juanmartinez@gmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[Instructor] OFF
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (1, 1, 200.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (2, 2, 100.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (2, 3, 100.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (2, 7, 250.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (3, 5, 100.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (3, 6, 150.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (3, 7, 250.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (4, 6, 150.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (4, 7, 250.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (5, 8, 100.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (5, 9, 150.0000, 5)
GO
INSERT [dbo].[Instructor_Especialidad] ([legajo], [codigo], [tarifa], [experiencia]) VALUES (6, 11, 800.0000, 2)
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

GO
INSERT [dbo].[Producto] ([id_producto], [destino], [actividad], [estado], [nombre], [precio], [duracion], [tipo_producto], [descripcion], [dificultad]) VALUES (1, N'', N'MONTAÑA', 1, N'MONT_MED_LXV_1400:1600', 1300.0000, 0, N'CURSO', N'Entrada en calor y movimientos-equilibrio avanzado.
Segundo tema de materiales y su correcta utilización.
Dar seguro con nudo dinámico y placa de manera confiable. Gri-gri
Escalar rutas de diferentes dificultades (según situación) en tope-rope.
Iniciación a la escalada en libre (escalar de primero-equipar en serguros fijos)
Colocación de materiales-empotradores-móviles
Nudos avanzados
Armado de triangulo de fuerzas y descuelgues
Rappel
Rutinas básicas y ejercicios prácticos.
Secretos para controlar el miedo y generar confianza.', N'MEDIO')
GO
INSERT [dbo].[Producto] ([id_producto], [destino], [actividad], [estado], [nombre], [precio], [duracion], [tipo_producto], [descripcion], [dificultad]) VALUES (2, N'', N'ORIENTACION Y GPS', 1, N'GPS_FAC_L_0900:1800', 1200.0000, 0, N'CURSO', N'Mapas y Cartografía, topografía.
GPS, SPOT y otros dispositivos
Coordenadas geográficas y planas
Orientación diurna y nocturna.
La brújula, el altímetro, el clinómetro y otros accesorios.
Curvas de nivel, equidistancia, medir distancias
Determinación de posiciones en el terreno
Triangulación.
Navegación terrestre.
Navegación satelital G.P.S. básica, spot.
Intercambio y edición de datos en una computadora', N'FACIL')
GO
GO
INSERT [dbo].[Producto] ([id_producto], [destino], [actividad], [estado], [nombre], [precio], [duracion], [tipo_producto], [descripcion], [dificultad]) VALUES (3, N'BA_CAÑ', N'Mountain Bike', 1, N'BIKE#FAC#BA_CAÑ#1', 800.0000, 1, N'EVENTO', N'Nos encontraremos a las 9 hs. en la estación de tren Cañuelas donde tendremos un amplio estacionamiento, para dejar nuestro vehículo.
Pasamos las vías del ferrocarril, una escuela, el campo San Pedro y a unos 8-10 kms. sale un camino hacia nuestra izquierda, bordeado por altos eucaliptos que debemos tomar para llegar a la vieja estación La Noria. En La Noria la vieja estación esta derrumbada e intrusada, hay una formación de cargas abandonada, el camino la rodea y cruza las vías hacia el sur.
Llegamos a una intersección de caminos. Doblamos a la derecha hacia Abbott.
Seguimos por un camino de tierra en buen estado, puede haber bastante barro en época de lluvias, poco tránsito vehícular. Llegamos a un cruce con el camino asfaltado de acceso a Abbott donde nos ingresamos al pueblo rural, con su estación de tren, su iglesia, la oficina de correo, la estancia El sueño. Nuestro camino sigue las vías ferroviarias del lado del pueblo, pasamos por algunas casas y quintas hasta que entramos en zona de campos sojeros, maíz, trigo, algunas arboledas, un camino muy poco transitado, bastante agreste, muchas aves en la zona.

Después de un buen trecho llegamos al cruce con la Ruta 41, Aquí podemos desviarnos a Francisco Berra, un poblado de diez casas, una escuela grande y una salita. Luego distintos caminos nos permiten tomar hacia San Miguel del Monte. Andaremos un camino de tierra consolidado, en muy buen estado que nos trasladara al corazón de Uribelarrea.

Recorreremos el pueblo, la plaza principal de raro diseño octogonal de la cual parten cuatro diagonales,  la iglesia inaugurada en 1890 en cuyo interior se conservan vitraux originales que se utilizaran en 1996 para la filmación de la película “Evita” de Alan Parker, la fabrica de cerveza La Uribeña, el cementerio abandonado.', N'FACIL')
GO
INSERT [dbo].[Producto] ([id_producto], [destino], [actividad], [estado], [nombre], [precio], [duracion], [tipo_producto], [descripcion], [dificultad]) VALUES (4, N'BA_TIG', N'Mountain Bike', 1, N'BIKE#MED#BA_TIG#1', 750.0000, 1, N'EVENTO', N'Nos encontraremos a las 9 hs. en la estación de tren Tigre donde tendremos un amplio estacionamiento(tiene costo), para dejar nuestro vehículo. 

Salidmos de Tigre, Camino de los Remeros, Ruta 27, NordDelta, Benavidez, Maschwitz, Escobar,Ruta 26, Villa la Ñata (donde almorzamos liviano), retorno hacia Tigre por ruta 25, recorrdio por tigre y finalizamos en RESTO BAR , donde daremos el cierraedisfrutando de una picada  y cerveza junto al rio.

Km Aprox.: 65 km
Caminos: 90 % ASFALTO, 10 %TIERRA', N'MEDIO')
GO
INSERT [dbo].[Producto] ([id_producto], [destino], [actividad], [estado], [nombre], [precio], [duracion], [tipo_producto], [descripcion], [dificultad]) VALUES (5, N'MDZ_SOS', N'Cabalgata', 1, N'CAB#FAC#MDZ_SOS#3', 13900.0000, 3, N'PAQUETE', N'Día 1 : Encuentro en El Sosneado, chequeo de equipo, a las 8.30 am. Ingreso al valle del Atuel Superior. 50 kilómetros de camino secundario en plena cordillera. Pasamos por la Laguna del Sosneado y el viejo hotel termal abandonado. Arribo al Puesto Araya a donde nos encontraremos con los caballos. Presentación con baqueanos e inicio de la cabalgata. Cruce del Río Atuel e ingreso al Valle de las Lágrimas. 3 / 4 horas de cabalgata hasta El Barroso, el campamento base junto al río homónimo. Almuerzo. Tarde libre para descansar y disfrutar del lugar.  Cena: Chivo asado con ensaladas. Pernocte en carpa

Día 2: Desayuno. En esta jornada nos proponempos alcanzar a caballo el lugar a donde cayó, en Octubre de 1972, el avión de los uruguayos. Luego de unas dos horas de grandes paisajes entre lagunas y valles glaciarios cruzamos el Río de las Lágrimas, casi en el nacimiento en el glaciar del mismo nombre. Desde este lugar comienza un largo e importante ascenso hasta los 3600 msnm, donde se hayan los restos del avión y el lugar conmemorativo. El lugar posee una energía muy particular. Cada uno lo disfruta  a su manera y rinde el homenaje que considere. Visitaremos también parte del glaciar, donde se encuentra una parte del tren de aterrizaje. Almuerzo de campo. Regreso al campamento. Cena de despedida en la montaña. Pernocte.

Día 3: Desayuno. Nos despedimos del campamento para iniciar el descenso a caballo hasta el Puesto de inicio. Traslado en 4X4 de regreso a El Sosneado. Fin de nuestros servicios y del viaje.

RECOMENDADOS DE COMO VIAJAR:

Es conveniente realizar una noche previa en la ciudad de San Rafael (dia anterior). Y desde allí los pasamos a buscar en transfer.', N'FACIL')
GO
INSERT [dbo].[Producto] ([id_producto], [destino], [actividad], [estado], [nombre], [precio], [duracion], [tipo_producto], [descripcion], [dificultad]) VALUES (6, N'NQN_SMA', N'Escalada en Roca,Trekking', 1, N'ROCA_TREK#DIF#NQN_SMA#3', 7650.0000, 3, N'PAQUETE', N'Día 1 (dia previo): Encuentro en San Martin de los Andes con el equipo de guías. Asesoramiento sobre equipo alquilado y revisión de equipo. Por la tarde. Preparativos generales.

Día 2: Traslado en vehículo charter al Paso Tromen ubicado al pie de la cara norte del Volcán Lanin, donde está el puesto del guardaparque que controlará el equipo de los integrantes de la expedición. En la primera etapa, luego de ascender 5 / 7 horas aproximadamente, llegamos a uno de los refugios de montaña ubicado a los 2350 m.s.n.m aproximadamente. Cena y noche en el refugio o en carpa (según disponibilidad).

Día 3: Bien temprano y luego de un abundante desayuno comenzaremos nuestro intento a cumbre. A partir de esta altura la marcha es por neveros permanentes, por lo cual es indispensable el uso de grampones y piquetas para caminar (el día anterior o previo al ascenso se dará una breve explicación de cómo utilizarlos y de su seguridad). Antes del medio día se llega a la cumbre (3776 m.s.n.m.). Luego de los festejos y la sesión de fotos correspondiente comienza el descenso hasta el refugio y de allí hasta la base. Traslado de regreso a San Martin de los Andes al alojamiento. Fin de nuestros servicios. ', N'DIFICIL')
GO
INSERT [dbo].[Producto] ([id_producto], [destino], [actividad], [estado], [nombre], [precio], [duracion], [tipo_producto], [descripcion], [dificultad]) VALUES (7, N'FMA_MON', N'Kayak', 1, N'KAY#MED#FMA_MON#4', 8600.0000, 4, N'PAQUETE', N'Dia 1: Encuentro en la ciudad de Formosa durante el día. Nos alojaremos en un pequeño hostal de la ciudad. Revisión de equipo, preparación, presentación de guias. Descanso. Saldremos a cenar por la ciudad (cena no incluida en el programa)

Día 2: Desayuno, traslado hasta el Riacho Monte Lindo sobre ruta 2 zona de Pastoril. Luego de una recepción y charla técnica nos adentramos en aguas del mágico Monte Lindo remamos por sus aguas abrazados/as por selvas en galerías y palmares, un descanso en costa para almuerzo y continuar hasta nuestra primera parada un lugar llamado “tres bocas”, el riacho Tatú Piré y el Monte lindo se encuentran y forman un paisaje sólo visto por aventureros/as, lugar de excelente pesca de costa. 
Iniciamos nuestro acampe con opción de pesca embarcada o de costa, fogón para compartir historias, un cielo estrellado y pernocte en carpas.

Día 3: Desayuno campestre, desarmado del campamento y continuamos remando por el sinuoso Monte Lindo hasta el mediodía donde se descansa en costa para compartir el almuerzo. Seguimos la aventura hasta la media tarde para hacer nuestra segunda parada campamentil, nuevamente la opción de pesca de las embarcaciones o de costa están presentes para disfrutar. Guitarreadas y el fogón de despedida del día serán el telón de fondo de una aventura en la provincia de Formosa. Pernocte. 

Día 4:  Desayuno ribereño y canotaje hasta el puente sobre Ruta 11 donde culmina nuestra travesía aproximadamente al medio día. Traslado a la ciudad de Formosa. Fin de nuestros servicios. Consultar por alojarse en el mismo hostal para regresar al día siguiente al lugar de residencia de cada uno.', N'MEDIO')
GO
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[Traslado] ON 

GO
INSERT [dbo].[Traslado] ([id_traslado], [razon_social], [cuit], [direccion], [ciudad], [tipo], [tarifa], [email], [capacidad], [telefono]) VALUES (1, N'Cabalgatas Andinas', N'30-70885379-4', N'Adolfo Calle 4171, Planta Alta', N'Villa Nueva - Guaymallen', N'CABALLO', 10000.0000, N'info@trekking-travel.com.ar', 5, N'54 0261 4210450')
GO
SET IDENTITY_INSERT [dbo].[Traslado] OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Detalle_Factura_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Factura_Factura] FOREIGN KEY([id_factura])
REFERENCES [dbo].[Factura] ([id_factura])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Detalle_Factura_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura] CHECK CONSTRAINT [FK_Detalle_Factura_Factura]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Especialidad_Especialidad]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]'))
ALTER TABLE [dbo].[Instructor_Especialidad]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Especialidad_Especialidad] FOREIGN KEY([codigo])
REFERENCES [dbo].[Especialidad] ([codigo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Especialidad_Especialidad]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]'))
ALTER TABLE [dbo].[Instructor_Especialidad] CHECK CONSTRAINT [FK_Instructor_Especialidad_Especialidad]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Especialidad_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]'))
ALTER TABLE [dbo].[Instructor_Especialidad]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Especialidad_Instructor] FOREIGN KEY([legajo])
REFERENCES [dbo].[Instructor] ([legajo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Especialidad_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]'))
ALTER TABLE [dbo].[Instructor_Especialidad] CHECK CONSTRAINT [FK_Instructor_Especialidad_Instructor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reserva_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reserva]'))
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Calendario] FOREIGN KEY([id_calendario])
REFERENCES [dbo].[Calendario] ([id_calendario])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reserva_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reserva]'))
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Calendario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Cliente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Cliente] ([id_cliente])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Cliente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Cliente]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Factura] FOREIGN KEY([id_factura])
REFERENCES [dbo].[Factura] ([id_factura])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Factura]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Reserva]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Reserva] FOREIGN KEY([codigo])
REFERENCES [dbo].[Reserva] ([codigo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Reserva]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Reserva]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Voucher]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Voucher] FOREIGN KEY([codigo])
REFERENCES [dbo].[Voucher] ([codigo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Venta_Voucher]') AND parent_object_id = OBJECT_ID(N'[dbo].[Venta]'))
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Voucher]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Voucher_Producto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Voucher]'))
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Producto] ([id_producto])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Voucher_Producto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Voucher]'))
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Producto]
GO
