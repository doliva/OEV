USE [master]
GO
/****** Object:  Database [BD_OEV_S]    Script Date: 26/11/2017 06:49:11 p.m. ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'BD_OEV_S')
DROP DATABASE [BD_OEV_S]
GO
/****** Object:  Database [BD_OEV_S]    Script Date: 26/11/2017 06:49:11 p.m. ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BD_OEV_S')
BEGIN
CREATE DATABASE [BD_OEV_S]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_OEV_S', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\BD_OEV_S.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BD_OEV_S_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\BD_OEV_S_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

GO
ALTER DATABASE [BD_OEV_S] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BD_OEV_S].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BD_OEV_S] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BD_OEV_S] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BD_OEV_S] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BD_OEV_S] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BD_OEV_S] SET ARITHABORT OFF 
GO
ALTER DATABASE [BD_OEV_S] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BD_OEV_S] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BD_OEV_S] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BD_OEV_S] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BD_OEV_S] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BD_OEV_S] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BD_OEV_S] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BD_OEV_S] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BD_OEV_S] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BD_OEV_S] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BD_OEV_S] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BD_OEV_S] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BD_OEV_S] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BD_OEV_S] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BD_OEV_S] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BD_OEV_S] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BD_OEV_S] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BD_OEV_S] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BD_OEV_S] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BD_OEV_S] SET  MULTI_USER 
GO
ALTER DATABASE [BD_OEV_S] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BD_OEV_S] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BD_OEV_S] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BD_OEV_S] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [BD_OEV_S]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bitacora](
	[id_bitacora] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[rol] [varchar](30) NULL,
	[fecha] [date] NULL,
	[evento] [varchar](30) NULL,
	[detalle] [varchar](100) NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[id_bitacora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Familia]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Familia](
	[id_familia] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Familia] PRIMARY KEY CLUSTERED 
(
	[id_familia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Familia_Patente]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia_Patente](
	[id_familia] [int] NOT NULL,
	[id_patente] [int] NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Familia_Patente] PRIMARY KEY CLUSTERED 
(
	[id_familia] ASC,
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patente]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patente](
	[id_patente] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Patente] PRIMARY KEY CLUSTERED 
(
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[id_rol] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](30) NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol_Familia]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Familia](
	[id_rol] [int] NOT NULL,
	[id_familia] [int] NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Rol_Familia] PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC,
	[id_familia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rol_Patente]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Patente](
	[id_rol] [int] NOT NULL,
	[id_patente] [int] NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Rol_Patente] PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC,
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rol_Usuario]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Usuario](
	[id_rol] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Rol_Usuario] PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC,
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[ciudad] [varchar](50) NULL,
	[clave] [varchar](30) NULL,
	[dvh] [varchar](50) NULL,
 CONSTRAINT [PK_Usuario_1] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario_Familia]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Familia](
	[id_usuario] [int] NOT NULL,
	[id_familia] [int] NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Usuario_Familia] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_familia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario_Patente]    Script Date: 26/11/2017 04:31:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Patente](
	[id_usuario] [int] NOT NULL,
	[id_patente] [int] NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Usuario_Patente] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_patente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT [dbo].[Familia] ON 

GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (1, N'GESTION BD', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (2, N'GESTION USUARIOS', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (3, N'CRIPTOGRAFIA', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (4, N'BITACORA', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (5, N'PERMISOS', 1)
GO
INSERT [dbo].[Familia] ([id_familia], [descripcion], [estado]) VALUES (6, N'DIGITO VERIFICADOR', 1)
GO
SET IDENTITY_INSERT [dbo].[Familia] OFF
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente], [estado]) VALUES (1, 1, 1)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente], [estado]) VALUES (1, 2, 1)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente], [estado]) VALUES (2, 3, 1)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente], [estado]) VALUES (2, 4, 1)
GO
INSERT [dbo].[Familia_Patente] ([id_familia], [id_patente], [estado]) VALUES (2, 5, 1)
GO
SET IDENTITY_INSERT [dbo].[Patente] ON 

GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (1, N'COPIA BD', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (2, N'RESTAURAR BD', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (3, N'AGREGAR USUARIO', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (4, N'EDITAR USUARIO', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (5, N'CONSULTAR USUARIO', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (6, N'HORIZONTAL', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (7, N'VERTICAL', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (8, N'CRIPTOGRAFIA', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (9, N'CONSULTAR BITACORA', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (10, N'FUNCIONES', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (11, N'ASIGNAR', 1)
GO
INSERT [dbo].[Patente] ([id_patente], [descripcion], [estado]) VALUES (12, N'CONSULTAR PERMISOS', 1)
GO
SET IDENTITY_INSERT [dbo].[Patente] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

GO
INSERT [dbo].[Rol] ([id_rol], [descripcion], [estado]) VALUES (1, N'ADMINISTRADOR  ', 1)
GO
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
INSERT [dbo].[Rol_Familia] ([id_rol], [id_familia], [estado]) VALUES (1, 1, 1)
GO
INSERT [dbo].[Rol_Familia] ([id_rol], [id_familia], [estado]) VALUES (1, 2, 1)
GO
INSERT [dbo].[Rol_Patente] ([id_rol], [id_patente], [estado]) VALUES (1, 1, 1)
GO
INSERT [dbo].[Rol_Patente] ([id_rol], [id_patente], [estado]) VALUES (1, 2, 0)
GO
INSERT [dbo].[Rol_Patente] ([id_rol], [id_patente], [estado]) VALUES (1, 3, 1)
GO
INSERT [dbo].[Rol_Patente] ([id_rol], [id_patente], [estado]) VALUES (1, 4, 1)
GO
INSERT [dbo].[Rol_Patente] ([id_rol], [id_patente], [estado]) VALUES (1, 5, 1)
GO
INSERT [dbo].[Rol_Usuario] ([id_rol], [id_usuario], [estado]) VALUES (1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

GO
INSERT [dbo].[Usuario] ([id_usuario], [nombre], [apellido], [dni], [fecha_nacimiento], [direccion], [telefono], [email], [estado], [ciudad], [clave], [dvh]) VALUES (1, N'Guillermo', N'Garmendia', N'34869472', CAST(0x420E0B00 AS Date), N'Av Congreso 1756, Buenos Aires', N'11 15 45937684', N'guillermo.garmendia@gmail.com', 1, N'Capital Federal', N'YQBkAG0AaQBuAA==', N'3vvtn0mHD7eqObgDuzIz9iMr6WFlllh/9RJDSLkoY1E=')
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente], [estado]) VALUES (1, 1, 1)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente], [estado]) VALUES (1, 2, 1)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente], [estado]) VALUES (1, 3, 1)
GO
INSERT [dbo].[Usuario_Patente] ([id_usuario], [id_patente], [estado]) VALUES (1, 4, 1)
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Usuario]
GO
ALTER TABLE [dbo].[Familia_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Familia_Patente_Familia] FOREIGN KEY([id_familia])
REFERENCES [dbo].[Familia] ([id_familia])
GO
ALTER TABLE [dbo].[Familia_Patente] CHECK CONSTRAINT [FK_Familia_Patente_Familia]
GO
ALTER TABLE [dbo].[Familia_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Familia_Patente_Patente] FOREIGN KEY([id_patente])
REFERENCES [dbo].[Patente] ([id_patente])
GO
ALTER TABLE [dbo].[Familia_Patente] CHECK CONSTRAINT [FK_Familia_Patente_Patente]
GO
ALTER TABLE [dbo].[Rol_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Familia_Familia] FOREIGN KEY([id_familia])
REFERENCES [dbo].[Familia] ([id_familia])
GO
ALTER TABLE [dbo].[Rol_Familia] CHECK CONSTRAINT [FK_Rol_Familia_Familia]
GO
ALTER TABLE [dbo].[Rol_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Familia_Rol] FOREIGN KEY([id_rol])
REFERENCES [dbo].[Rol] ([id_rol])
GO
ALTER TABLE [dbo].[Rol_Familia] CHECK CONSTRAINT [FK_Rol_Familia_Rol]
GO
ALTER TABLE [dbo].[Rol_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Patente_Patente] FOREIGN KEY([id_patente])
REFERENCES [dbo].[Patente] ([id_patente])
GO
ALTER TABLE [dbo].[Rol_Patente] CHECK CONSTRAINT [FK_Rol_Patente_Patente]
GO
ALTER TABLE [dbo].[Rol_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Patente_Rol] FOREIGN KEY([id_rol])
REFERENCES [dbo].[Rol] ([id_rol])
GO
ALTER TABLE [dbo].[Rol_Patente] CHECK CONSTRAINT [FK_Rol_Patente_Rol]
GO
ALTER TABLE [dbo].[Rol_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Usuario_Rol] FOREIGN KEY([id_rol])
REFERENCES [dbo].[Rol] ([id_rol])
GO
ALTER TABLE [dbo].[Rol_Usuario] CHECK CONSTRAINT [FK_Rol_Usuario_Rol]
GO
ALTER TABLE [dbo].[Rol_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Rol_Usuario_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Rol_Usuario] CHECK CONSTRAINT [FK_Rol_Usuario_Usuario]
GO
ALTER TABLE [dbo].[Usuario_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Familia_Familia] FOREIGN KEY([id_familia])
REFERENCES [dbo].[Familia] ([id_familia])
GO
ALTER TABLE [dbo].[Usuario_Familia] CHECK CONSTRAINT [FK_Usuario_Familia_Familia]
GO
ALTER TABLE [dbo].[Usuario_Familia]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Familia_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Usuario_Familia] CHECK CONSTRAINT [FK_Usuario_Familia_Usuario]
GO
ALTER TABLE [dbo].[Usuario_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Patente_Patente] FOREIGN KEY([id_patente])
REFERENCES [dbo].[Patente] ([id_patente])
GO
ALTER TABLE [dbo].[Usuario_Patente] CHECK CONSTRAINT [FK_Usuario_Patente_Patente]
GO
ALTER TABLE [dbo].[Usuario_Patente]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Patente_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Usuario_Patente] CHECK CONSTRAINT [FK_Usuario_Patente_Usuario]
GO
USE [master]
GO
ALTER DATABASE [BD_OEV_S] SET  READ_WRITE 
GO
