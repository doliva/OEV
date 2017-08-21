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
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Patente_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Patente]'))
ALTER TABLE [dbo].[Usuario_Patente] DROP CONSTRAINT [FK_Usuario_Patente_Usuario]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Patente_Patente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Patente]'))
ALTER TABLE [dbo].[Usuario_Patente] DROP CONSTRAINT [FK_Usuario_Patente_Patente]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Familia_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Familia]'))
ALTER TABLE [dbo].[Usuario_Familia] DROP CONSTRAINT [FK_Usuario_Familia_Familia]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Familia]'))
ALTER TABLE [dbo].[Usuario_Familia] DROP CONSTRAINT [FK_Usuario_Familia]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reserva_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reserva]'))
ALTER TABLE [dbo].[Reserva] DROP CONSTRAINT [FK_Reserva_Calendario]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Producto_Actividad]') AND parent_object_id = OBJECT_ID(N'[dbo].[Producto]'))
ALTER TABLE [dbo].[Producto] DROP CONSTRAINT [FK_Producto_Actividad]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Especialidad_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]'))
ALTER TABLE [dbo].[Instructor_Especialidad] DROP CONSTRAINT [FK_Instructor_Especialidad_Instructor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Especialidad_Especialidad]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]'))
ALTER TABLE [dbo].[Instructor_Especialidad] DROP CONSTRAINT [FK_Instructor_Especialidad_Especialidad]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Familia_Patente_Patente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Familia_Patente]'))
ALTER TABLE [dbo].[Familia_Patente] DROP CONSTRAINT [FK_Familia_Patente_Patente]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Familia_Patente_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Familia_Patente]'))
ALTER TABLE [dbo].[Familia_Patente] DROP CONSTRAINT [FK_Familia_Patente_Familia]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Detalle_Factura_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura] DROP CONSTRAINT [FK_Detalle_Factura_Factura]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Traslado_Traslado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Traslado]'))
ALTER TABLE [dbo].[Calendario_Traslado] DROP CONSTRAINT [FK_Calendario_Traslado_Traslado]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Traslado_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Traslado]'))
ALTER TABLE [dbo].[Calendario_Traslado] DROP CONSTRAINT [FK_Calendario_Traslado_Calendario]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Instructor_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Instructor]'))
ALTER TABLE [dbo].[Calendario_Instructor] DROP CONSTRAINT [FK_Calendario_Instructor_Instructor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Instructor_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Instructor]'))
ALTER TABLE [dbo].[Calendario_Instructor] DROP CONSTRAINT [FK_Calendario_Instructor_Calendario]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Alojamiento_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Alojamiento]'))
ALTER TABLE [dbo].[Calendario_Alojamiento] DROP CONSTRAINT [FK_Calendario_Alojamiento_Calendario]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Alojamiento_Alojamiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Alojamiento]'))
ALTER TABLE [dbo].[Calendario_Alojamiento] DROP CONSTRAINT [FK_Calendario_Alojamiento_Alojamiento]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Producto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario]'))
ALTER TABLE [dbo].[Calendario] DROP CONSTRAINT [FK_Calendario_Producto]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bitacora_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bitacora]'))
ALTER TABLE [dbo].[Bitacora] DROP CONSTRAINT [FK_Bitacora_Usuario]
GO
/****** Object:  Index [IX_Detalle_Factura]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]') AND name = N'IX_Detalle_Factura')
DROP INDEX [IX_Detalle_Factura] ON [dbo].[Detalle_Factura]
GO
/****** Object:  Index [IX_Calendario]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Calendario]') AND name = N'IX_Calendario')
DROP INDEX [IX_Calendario] ON [dbo].[Calendario]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Voucher]') AND type in (N'U'))
DROP TABLE [dbo].[Voucher]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Venta]') AND type in (N'U'))
DROP TABLE [dbo].[Venta]
GO
/****** Object:  Table [dbo].[Usuario_Patente]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario_Patente]') AND type in (N'U'))
DROP TABLE [dbo].[Usuario_Patente]
GO
/****** Object:  Table [dbo].[Usuario_Familia]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario_Familia]') AND type in (N'U'))
DROP TABLE [dbo].[Usuario_Familia]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
DROP TABLE [dbo].[Usuario]
GO
/****** Object:  Table [dbo].[Traslado]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Traslado]') AND type in (N'U'))
DROP TABLE [dbo].[Traslado]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reserva]') AND type in (N'U'))
DROP TABLE [dbo].[Reserva]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Producto]') AND type in (N'U'))
DROP TABLE [dbo].[Producto]
GO
/****** Object:  Table [dbo].[Patente]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patente]') AND type in (N'U'))
DROP TABLE [dbo].[Patente]
GO
/****** Object:  Table [dbo].[Instructor_Especialidad]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor_Especialidad]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor_Especialidad]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor]
GO
/****** Object:  Table [dbo].[Familia_Patente]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Familia_Patente]') AND type in (N'U'))
DROP TABLE [dbo].[Familia_Patente]
GO
/****** Object:  Table [dbo].[Familia]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Familia]') AND type in (N'U'))
DROP TABLE [dbo].[Familia]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Factura]') AND type in (N'U'))
DROP TABLE [dbo].[Factura]
GO
/****** Object:  Table [dbo].[Especialidad]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Especialidad]') AND type in (N'U'))
DROP TABLE [dbo].[Especialidad]
GO
/****** Object:  Table [dbo].[Detalle_Factura]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]') AND type in (N'U'))
DROP TABLE [dbo].[Detalle_Factura]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type in (N'U'))
DROP TABLE [dbo].[Cliente]
GO
/****** Object:  Table [dbo].[Calendario_Traslado]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario_Traslado]') AND type in (N'U'))
DROP TABLE [dbo].[Calendario_Traslado]
GO
/****** Object:  Table [dbo].[Calendario_Instructor]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario_Instructor]') AND type in (N'U'))
DROP TABLE [dbo].[Calendario_Instructor]
GO
/****** Object:  Table [dbo].[Calendario_Alojamiento]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario_Alojamiento]') AND type in (N'U'))
DROP TABLE [dbo].[Calendario_Alojamiento]
GO
/****** Object:  Table [dbo].[Calendario]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario]') AND type in (N'U'))
DROP TABLE [dbo].[Calendario]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bitacora]') AND type in (N'U'))
DROP TABLE [dbo].[Bitacora]
GO
/****** Object:  Table [dbo].[Alojamiento]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Alojamiento]') AND type in (N'U'))
DROP TABLE [dbo].[Alojamiento]
GO
/****** Object:  Table [dbo].[Actividad]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actividad]') AND type in (N'U'))
DROP TABLE [dbo].[Actividad]
GO
USE [master]
GO
/****** Object:  Database [BD_OEV_C]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'BD_OEV_C')
DROP DATABASE [BD_OEV_C]
GO
/****** Object:  Database [BD_OEV_C]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BD_OEV_C')
BEGIN
CREATE DATABASE [BD_OEV_C]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_OEV_C', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\BD_OEV_C.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BD_OEV_C_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\BD_OEV_C_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

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
USE [BD_OEV_C]
GO
/****** Object:  Table [dbo].[Actividad]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actividad]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Actividad](
	[id_actividad] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[dias] [varchar](100) NULL,
	[horarios] [varchar](50) NULL,
	[nombre] [varchar](20) NULL,
	[dificultad] [varchar](20) NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Actividad] PRIMARY KEY CLUSTERED 
(
	[id_actividad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Alojamiento]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
 CONSTRAINT [PK_Alojamiento] PRIMARY KEY CLUSTERED 
(
	[id_alojamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bitacora]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Bitacora](
	[id_bitacora] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[rol] [varchar](15) NULL,
	[fecha] [date] NULL,
	[evento] [varchar](30) NULL,
	[detalle] [varchar](100) NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[id_bitacora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Calendario]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Calendario](
	[id_calendario] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [int] NOT NULL,
	[cupo] [int] NULL,
	[fecha_salida] [date] NULL,
	[fecha_regreso] [date] NULL,
	[precio] [money] NULL,
 CONSTRAINT [PK_Calendario] PRIMARY KEY CLUSTERED 
(
	[id_calendario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Calendario_Alojamiento]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario_Alojamiento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Calendario_Alojamiento](
	[id_calendario] [int] NOT NULL,
	[id_alojamiento] [int] NOT NULL,
 CONSTRAINT [PK_Calendario_Alojamiento] PRIMARY KEY CLUSTERED 
(
	[id_calendario] ASC,
	[id_alojamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Calendario_Instructor]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario_Instructor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Calendario_Instructor](
	[legajo] [int] NOT NULL,
	[id_calendario] [int] NOT NULL,
 CONSTRAINT [PK_Calendario_Instructor] PRIMARY KEY CLUSTERED 
(
	[legajo] ASC,
	[id_calendario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Calendario_Traslado]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calendario_Traslado]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Calendario_Traslado](
	[id_calendario] [int] NOT NULL,
	[id_traslado] [int] NOT NULL,
 CONSTRAINT [PK_Calendario_Traslado] PRIMARY KEY CLUSTERED 
(
	[id_calendario] ASC,
	[id_traslado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Detalle_Factura]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Especialidad]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Factura]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Familia]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Familia]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Familia](
	[id_familia] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Familia] PRIMARY KEY CLUSTERED 
(
	[id_familia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Familia_Patente]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Familia_Patente]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Familia_Patente](
	[id_familia] [int] NOT NULL,
	[id_patente] [int] NOT NULL,
 CONSTRAINT [PK_Familia_Patente] PRIMARY KEY CLUSTERED 
(
	[id_familia] ASC,
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Instructor_Especialidad]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Patente]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patente]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Patente](
	[id_patente] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Patente] PRIMARY KEY CLUSTERED 
(
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
	[id_actividad] [int] NOT NULL,
	[destino] [varchar](100) NULL,
	[estado] [bit] NULL,
	[itinerario] [varchar](1000) NULL,
	[nombre] [varchar](100) NULL,
	[precio] [money] NULL,
	[tipo_producto] [varchar](20) NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Traslado]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NULL,
	[apellido] [varchar](30) NULL,
	[dni] [varchar](10) NOT NULL,
	[fecha_nacimiento] [date] NULL,
	[direccion] [varchar](100) NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](30) NULL,
	[estado] [bit] NOT NULL,
	[rol] [varchar](15) NULL,
	[ciudad] [varchar](50) NULL,
	[clave] [varchar](30) NULL,
	[dvh] [varchar](50) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario_Familia]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario_Familia]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuario_Familia](
	[id_usuario] [int] NOT NULL,
	[id_familia] [int] NOT NULL,
 CONSTRAINT [PK_Usuario_Familia] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_familia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Usuario_Patente]    Script Date: 08/08/2017 02:21:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario_Patente]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuario_Patente](
	[id_usuario] [int] NOT NULL,
	[id_patente] [int] NOT NULL,
 CONSTRAINT [PK_Usuario_Patente] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
/****** Object:  Table [dbo].[Voucher]    Script Date: 08/08/2017 02:21:17 p.m. ******/
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
SET IDENTITY_INSERT [dbo].[Especialidad] ON 

GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (1, N'Instructor de escalada en roca                    ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (2, N'Instructor de escalada en hielo                   ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (3, N'Guia profesional                                  ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (4, N'Profesor de Educación Física                      ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (5, N'Socorrista                                        ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (6, N'Guía profesional de trekking en cordillera        ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (7, N'Instructor de andinismo                           ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (8, N'Instructor de Primeros Socorros                   ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (9, N'Instructor de RCP                                 ')
GO
INSERT [dbo].[Especialidad] ([codigo], [descripcion]) VALUES (10, N'Guía de Cabalgatas                                ')
GO
SET IDENTITY_INSERT [dbo].[Especialidad] OFF
GO
SET IDENTITY_INSERT [dbo].[Familia] ON 

GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (1, N'ADMINISTRADOR', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (2, N'OPERACIONES', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (3, N'COMERCIAL', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (4, N'CLIENTE', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (5, N'DIRECCION', 1)
GO
SET IDENTITY_INSERT [dbo].[Familia] OFF
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 1)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 2)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 3)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 4)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 5)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 6)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 7)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 8)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 9)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 10)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente]) VALUES (1, 11)
GO
SET IDENTITY_INSERT [dbo].[Instructor] ON 

GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (1, N'29356231  ', N'Leandro                       ', N'Robledo                       ', N'Amenabar 2314, Buenos Aires                       ', N'Buenos Aires', N'11 15 48769023      ', N'leandro.robledo@gmail.com     ', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (2, N'31769256  ', N'Fernando                      ', N'Scheuler                      ', N'Ciudad de la Paz 1589, Buenos Aires               ', N'Buenos Aires', N'11 15 48973672      ', N'fernando.scheuler@gmail.com   ', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (3, N'33806835  ', N'David                         ', N'Rivelli                       ', N'Av Cabildo 3546, Buenos Aires                     ', N'Buenos Aires', N'11 15 67543879      ', N'david.rivelli@gmail.com       ', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (4, N'28593275  ', N'Ivan                          ', N'Calleri                       ', N'Cuba 1845, Buenos Aires                           ', N'Buenos Aires', N'11 15 54386054      ', N'ivan.calleri@gmail.com        ', 1)
GO
INSERT [dbo].[Instructor] ([legajo], [dni], [nombre], [apellido], [domicilio], [ciudad], [telefono], [email], [estado]) VALUES (5, N'32596235  ', N'German                        ', N'Olivares                      ', N'Moldes 1245, Buenos Aires                         ', N'Buenos Aires', N'11 15 59506724      ', N'german.olivares@gmail.com     ', 1)
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
SET IDENTITY_INSERT [dbo].[Patente] ON 

GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (1, N'Alta Usuario', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (2, N'Editar Usuario', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (3, N'Consultar Usuario', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (4, N'Restore db', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (5, N'Alta Familia', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (6, N'Editar Familia', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (7, N'Consultar Familia', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (8, N'Alta Patente', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (9, N'Editar Patente', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (10, N'Consultar Patente', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (11, N'Consultar Bitacora', 1)
GO
SET IDENTITY_INSERT [dbo].[Patente] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (1, N'Guillermo                     ', N'Garmendia                     ', N'34869472  ', CAST(0x420E0B00 AS Date), N'Av Congreso 1756, Buenos Aires                    ', N'11 15 45937684      ', N'guillermo.garmendia@gmail.com ', 1, N'ADMINISTRADOR  ', N'Capital Federal', N'YQBkAG0AaQBuAA==', N'3vvtn0mHD7eqObgDuzIz9iMr6WFlllh/9RJDSLkoY1E=')
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (2, N'Mariano                       ', N'Garay                         ', N'29869472  ', CAST(0x54070B00 AS Date), N'Mendoza 2759, Buenos Aires                        ', N'11 15 65928686      ', N'mariano.garay@gmail.com       ', 1, N'CLIENTE        ', N'Capital Federal', NULL, NULL)
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (3, N'Gustavo                       ', N'Montoya                       ', N'32863772  ', CAST(0xA50A0B00 AS Date), N'Zabala 2361, Buenos Aires                         ', N'11 15 55837617      ', N'gustavo.montoya@gmail.com     ', 1, N'CLIENTE        ', N'Capital Federal', NULL, NULL)
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (4, N'Monica                        ', N'Palacios                      ', N'35864072  ', CAST(0xAF0E0B00 AS Date), N'Arcos 689, Buenos Aires                           ', N'11 15 4963684       ', N'monica.palacios@gmail.com     ', 1, N'CLIENTE        ', N'Capital Federal', NULL, NULL)
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (5, N'Juan                          ', N'Ayala                         ', N'31865922  ', CAST(0x850C0B00 AS Date), N'Echeverria 589, Buenos Aires                      ', N'11 15 63937204      ', N'juan.ayala@gmail.com          ', 1, N'CLIENTE        ', N'Capital Federal', NULL, NULL)
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (6, N'Ariel                         ', N'Duarte                        ', N'36860482  ', CAST(0x09120B00 AS Date), N'Vidal 2187, Buenos Aires                          ', N'11 15 48527680      ', N'ariel.duarte@gmail.com        ', 1, N'CLIENTE        ', N'Capital Federal', NULL, NULL)
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (7, N'Cristian                      ', N'Silva                         ', N'28874470  ', CAST(0x6A080B00 AS Date), N'La Pampa 874, Buenos Aires                        ', N'11 15 62934024      ', N'cristian.silva@gmail.com      ', 1, N'CLIENTE        ', N'Capital Federal', NULL, NULL)
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (8, N'Romina                        ', N'Paladino                      ', N'30860262  ', CAST(0x010B0B00 AS Date), N'Zapiola 2356, Buenos Aires                        ', N'11 15 45905834      ', N'romina.paladino@gmail.com     ', 1, N'CLIENTE        ', N'Capital Federal', NULL, NULL)
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (9, N'Melisa', N'Sosa', N'22459531', CAST(0x2CFE0A00 AS Date), N'La Pampa 353', N'45892398', N'melisa.sosa@gmail.com', 0, N'OPERACIONES', N'Buenos Aires', N'MgAyADQANQA5ADUAMwAxAA==', N'QtD5Ga6Ka7uL7wJuyy4QSUYq1Bk5odOx3A/xe7z1ZiQ=')
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (10, N'Melina', N'Juarez', N'22367275', CAST(0x07FE0A00 AS Date), N'Cordoba 583', N'45629873', N'melina.juarez@gmail.com', 1, N'OPERACIONES', N'Buenos Aires', N'MgAyADMANgA3ADIANwA1AA==', N'3neRiPsl6jjM51cWICzMnqzb2OvZ5l0xwhmGpplRvOU=')
GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [rol], [ciudad], [clave], [dvh]) VALUES (11, N'Patricia', N'Morales', N'23784945', CAST(0xBBFF0A00 AS Date), N'Andes 3478', N'47329832', N'patricia.morales@gmail.com', 1, N'COMERCIAL', N'Buenos Aires', N'MgAzADcAOAA0ADkANAA1AA==', N'LjsQ6NC+oeRMwxC68F6eFoifwIhegfo9CRzlWtFpOT4=')
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (1, 1)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (2, 4)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (3, 4)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (4, 4)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (5, 4)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (6, 4)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (7, 4)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (8, 4)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (9, 2)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (10, 2)
GO
INSERT [dbo].[Usuario_Familia] ([id_usuario], [id_familia]) VALUES (11, 3)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 1)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 2)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 3)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 4)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 5)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 6)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 7)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 8)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 9)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 10)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente]) VALUES (1, 11)
GO
/****** Object:  Index [IX_Calendario]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Calendario]') AND name = N'IX_Calendario')
CREATE NONCLUSTERED INDEX [IX_Calendario] ON [dbo].[Calendario]
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Detalle_Factura]    Script Date: 08/08/2017 02:21:17 p.m. ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]') AND name = N'IX_Detalle_Factura')
CREATE NONCLUSTERED INDEX [IX_Detalle_Factura] ON [dbo].[Detalle_Factura]
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bitacora_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bitacora]'))
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bitacora_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bitacora]'))
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Usuario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Producto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario]'))
ALTER TABLE [dbo].[Calendario]  WITH CHECK ADD  CONSTRAINT [FK_Calendario_Producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Producto] ([id_producto])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Producto]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario]'))
ALTER TABLE [dbo].[Calendario] CHECK CONSTRAINT [FK_Calendario_Producto]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Alojamiento_Alojamiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Alojamiento]'))
ALTER TABLE [dbo].[Calendario_Alojamiento]  WITH CHECK ADD  CONSTRAINT [FK_Calendario_Alojamiento_Alojamiento] FOREIGN KEY([id_alojamiento])
REFERENCES [dbo].[Alojamiento] ([id_alojamiento])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Alojamiento_Alojamiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Alojamiento]'))
ALTER TABLE [dbo].[Calendario_Alojamiento] CHECK CONSTRAINT [FK_Calendario_Alojamiento_Alojamiento]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Alojamiento_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Alojamiento]'))
ALTER TABLE [dbo].[Calendario_Alojamiento]  WITH CHECK ADD  CONSTRAINT [FK_Calendario_Alojamiento_Calendario] FOREIGN KEY([id_calendario])
REFERENCES [dbo].[Calendario] ([id_calendario])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Alojamiento_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Alojamiento]'))
ALTER TABLE [dbo].[Calendario_Alojamiento] CHECK CONSTRAINT [FK_Calendario_Alojamiento_Calendario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Instructor_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Instructor]'))
ALTER TABLE [dbo].[Calendario_Instructor]  WITH CHECK ADD  CONSTRAINT [FK_Calendario_Instructor_Calendario] FOREIGN KEY([id_calendario])
REFERENCES [dbo].[Calendario] ([id_calendario])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Instructor_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Instructor]'))
ALTER TABLE [dbo].[Calendario_Instructor] CHECK CONSTRAINT [FK_Calendario_Instructor_Calendario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Instructor_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Instructor]'))
ALTER TABLE [dbo].[Calendario_Instructor]  WITH CHECK ADD  CONSTRAINT [FK_Calendario_Instructor_Instructor] FOREIGN KEY([legajo])
REFERENCES [dbo].[Instructor] ([legajo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Instructor_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Instructor]'))
ALTER TABLE [dbo].[Calendario_Instructor] CHECK CONSTRAINT [FK_Calendario_Instructor_Instructor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Traslado_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Traslado]'))
ALTER TABLE [dbo].[Calendario_Traslado]  WITH CHECK ADD  CONSTRAINT [FK_Calendario_Traslado_Calendario] FOREIGN KEY([id_calendario])
REFERENCES [dbo].[Calendario] ([id_calendario])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Traslado_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Traslado]'))
ALTER TABLE [dbo].[Calendario_Traslado] CHECK CONSTRAINT [FK_Calendario_Traslado_Calendario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Traslado_Traslado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Traslado]'))
ALTER TABLE [dbo].[Calendario_Traslado]  WITH CHECK ADD  CONSTRAINT [FK_Calendario_Traslado_Traslado] FOREIGN KEY([id_traslado])
REFERENCES [dbo].[Traslado] ([id_traslado])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calendario_Traslado_Traslado]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calendario_Traslado]'))
ALTER TABLE [dbo].[Calendario_Traslado] CHECK CONSTRAINT [FK_Calendario_Traslado_Traslado]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Detalle_Factura_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Factura_Factura] FOREIGN KEY([id_factura])
REFERENCES [dbo].[Factura] ([id_factura])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Detalle_Factura_Factura]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura] CHECK CONSTRAINT [FK_Detalle_Factura_Factura]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Familia_Patente_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Familia_Patente]'))
ALTER TABLE [dbo].[Familia_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Familia_Patente_Familia] FOREIGN KEY([id_familia])
REFERENCES [dbo].[Familia] ([id_familia])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Familia_Patente_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Familia_Patente]'))
ALTER TABLE [dbo].[Familia_Patente] CHECK CONSTRAINT [FK_Familia_Patente_Familia]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Familia_Patente_Patente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Familia_Patente]'))
ALTER TABLE [dbo].[Familia_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Familia_Patente_Patente] FOREIGN KEY([id_patente])
REFERENCES [dbo].[Patente] ([id_patente])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Familia_Patente_Patente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Familia_Patente]'))
ALTER TABLE [dbo].[Familia_Patente] CHECK CONSTRAINT [FK_Familia_Patente_Patente]
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
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Producto_Actividad]') AND parent_object_id = OBJECT_ID(N'[dbo].[Producto]'))
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Actividad] FOREIGN KEY([id_actividad])
REFERENCES [dbo].[Actividad] ([id_actividad])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Producto_Actividad]') AND parent_object_id = OBJECT_ID(N'[dbo].[Producto]'))
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Actividad]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reserva_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reserva]'))
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Calendario] FOREIGN KEY([id_calendario])
REFERENCES [dbo].[Calendario] ([id_calendario])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Reserva_Calendario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Reserva]'))
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Calendario]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Familia]'))
ALTER TABLE [dbo].[Usuario_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Familia] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Familia]'))
ALTER TABLE [dbo].[Usuario_Familia] CHECK CONSTRAINT [FK_Usuario_Familia]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Familia_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Familia]'))
ALTER TABLE [dbo].[Usuario_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Familia_Familia] FOREIGN KEY([id_familia])
REFERENCES [dbo].[Familia] ([id_familia])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Familia_Familia]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Familia]'))
ALTER TABLE [dbo].[Usuario_Familia] CHECK CONSTRAINT [FK_Usuario_Familia_Familia]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Patente_Patente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Patente]'))
ALTER TABLE [dbo].[Usuario_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Patente_Patente] FOREIGN KEY([id_patente])
REFERENCES [dbo].[Patente] ([id_patente])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Patente_Patente]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Patente]'))
ALTER TABLE [dbo].[Usuario_Patente] CHECK CONSTRAINT [FK_Usuario_Patente_Patente]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Patente_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Patente]'))
ALTER TABLE [dbo].[Usuario_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Patente_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Usuario_Patente_Usuario]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario_Patente]'))
ALTER TABLE [dbo].[Usuario_Patente] CHECK CONSTRAINT [FK_Usuario_Patente_Usuario]
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
USE [master]
GO
ALTER DATABASE [BD_OEV_C] SET  READ_WRITE 
GO
