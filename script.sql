USE [master]
GO
/****** Object:  Database [Study_department]    Script Date: 27.12.2023 19:52:48 ******/
CREATE DATABASE [Study_department]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Study_department', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Study_department.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Study_department_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Study_department_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Study_department] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Study_department].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Study_department] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Study_department] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Study_department] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Study_department] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Study_department] SET ARITHABORT OFF 
GO
ALTER DATABASE [Study_department] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Study_department] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Study_department] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Study_department] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Study_department] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Study_department] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Study_department] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Study_department] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Study_department] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Study_department] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Study_department] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Study_department] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Study_department] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Study_department] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Study_department] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Study_department] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Study_department] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Study_department] SET RECOVERY FULL 
GO
ALTER DATABASE [Study_department] SET  MULTI_USER 
GO
ALTER DATABASE [Study_department] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Study_department] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Study_department] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Study_department] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Study_department] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Study_department] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Study_department', N'ON'
GO
ALTER DATABASE [Study_department] SET QUERY_STORE = OFF
GO
USE [Study_department]
GO
/****** Object:  Table [dbo].[Autorization]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autorization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[RefreshToken] [nvarchar](250) NULL,
	[RefreshTokenExp] [datetime] NULL,
	[Id_Role] [int] NOT NULL,
	[Image] [nvarchar](50) NULL,
 CONSTRAINT [PK_Autorization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cabinets]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cabinets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Cabinet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Changes]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Changes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Teacher] [int] NOT NULL,
	[Id_Group] [int] NOT NULL,
	[Date] [date] NULL,
	[Id_Subject] [int] NOT NULL,
	[Id_Number_pair] [int] NOT NULL,
	[Id_Cabinets] [int] NOT NULL,
 CONSTRAINT [PK_Changes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Day_week]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Day_week](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Day_week] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Id_Status_group] [int] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Number_pair]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Number_pair](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Number_pair] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Teacher] [int] NOT NULL,
	[Id_Group] [int] NOT NULL,
	[Id_Day_week] [int] NOT NULL,
	[Id_Subject] [int] NOT NULL,
	[Id_Number_pair] [int] NOT NULL,
	[Id_Cabinets] [int] NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status_group]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status_group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status_group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status_teacher]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status_teacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status_teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Family] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Id_Type_employment] [int] NOT NULL,
	[Id_Status_teacher] [int] NOT NULL,
	[Image] [nvarchar](50) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type_employment]    Script Date: 27.12.2023 19:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type_employment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Type_employment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Autorization] ON 

INSERT [dbo].[Autorization] ([Id], [Login], [Password], [RefreshToken], [RefreshTokenExp], [Id_Role], [Image]) VALUES (1, N'workeerr', N'AQAAAAIAAYagAAAAECMf3t6LNEj9QlrGHtPxcTLAcaso+ySFwAijX1TcwZDzUJhSgI9mB5sVMWCC40DPCg==', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE3MDM2NzEwNTIsImV4cCI6MTcwODg1NTA1MiwiaXNzIjoiU3R1ZHlfZGVwYXJ0bWVudElTU2VydmVyIiwiYXVkIjoiU3R1ZHlfZGVwYXJ0bWVudElTV29ya2VyIn0.AQnPzGnxHBWZutvNiZddj7lAqBJusKx8czEtuj7UysU', CAST(N'2024-02-25T12:57:32.217' AS DateTime), 1, N'woman.jpg')
INSERT [dbo].[Autorization] ([Id], [Login], [Password], [RefreshToken], [RefreshTokenExp], [Id_Role], [Image]) VALUES (3, N'tyty', N'paroljvkjvukgkhckhgchhckhchcg', NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Autorization] OFF
GO
SET IDENTITY_INSERT [dbo].[Cabinets] ON 

INSERT [dbo].[Cabinets] ([Id], [Number], [Name], [Type]) VALUES (1, 412, N'Для Ворда', N'ПК')
INSERT [dbo].[Cabinets] ([Id], [Number], [Name], [Type]) VALUES (2, 418, N'Для Кодинга', N'СУПЕР-ПК')
INSERT [dbo].[Cabinets] ([Id], [Number], [Name], [Type]) VALUES (3, 408, N'ДЛЯ КЕО', N'СУПЕР-ДУПЕР-ПК-КЛУБ')
SET IDENTITY_INSERT [dbo].[Cabinets] OFF
GO
SET IDENTITY_INSERT [dbo].[Changes] ON 

INSERT [dbo].[Changes] ([Id], [Id_Teacher], [Id_Group], [Date], [Id_Subject], [Id_Number_pair], [Id_Cabinets]) VALUES (1, 8, 1, CAST(N'2023-12-21' AS Date), 1, 1, 1)
INSERT [dbo].[Changes] ([Id], [Id_Teacher], [Id_Group], [Date], [Id_Subject], [Id_Number_pair], [Id_Cabinets]) VALUES (2, 33, 2, CAST(N'2023-12-22' AS Date), 2, 2, 2)
INSERT [dbo].[Changes] ([Id], [Id_Teacher], [Id_Group], [Date], [Id_Subject], [Id_Number_pair], [Id_Cabinets]) VALUES (3, 27, 70, CAST(N'2023-12-23' AS Date), 3, 3, 3)
SET IDENTITY_INSERT [dbo].[Changes] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 

INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (1, N'20БИО', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (2, N'20ИП1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (3, N'20ИП2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (4, N'20КС', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (5, N'20МТ', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (6, N'20С', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (7, N'20САТ1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (8, N'20САТ2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (9, N'20ТА1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (10, N'20ТА2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (11, N'20ЭК', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (12, N'20Ю', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (13, N'21БИО', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (14, N'21ДК', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (15, N'21ИП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (16, N'21ИПВ', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (17, N'21МТ', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (18, N'21Н', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (19, N'21НП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (20, N'21С', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (21, N'21САТ1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (22, N'21САТ2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (23, N'21ТА1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (24, N'21ТА2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (25, N'21ЭК 22ЭКП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (26, N'21Ю', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (27, N'21ЮП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (28, N'22БИОп', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (29, N'22ДК', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (30, N'22ДКП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (31, N'22ИП1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (32, N'22ИП2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (33, N'22ИПВ', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (34, N'22ИПП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (35, N'22МТ1п', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (36, N'22МТ2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (37, N'22Н1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (38, N'22Н2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (39, N'22НП1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (40, N'22НП2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (41, N'22С', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (42, N'22САТ1п', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (43, N'22САТ2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (44, N'22ТА1п', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (45, N'22ТА2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (46, N'22ЭК', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (47, N'22Ю1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (48, N'22Ю2 23ЮП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (49, N'22ЮП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (50, N'23БИОп', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (51, N'23БИОПп', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (52, N'23ДК', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (53, N'23ДКП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (54, N'23ИП1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (55, N'23ИП2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (56, N'23ИПВ', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (57, N'23ИПП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (58, N'23МТ1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (59, N'23МТ2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (60, N'23Н1', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (61, N'23Н2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (62, N'23НП', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (63, N'23С', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (64, N'23САТ1п', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (65, N'23САТ2', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (66, N'23ТА1п', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (67, N'23ТА2п', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (68, N'23ЭК', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (69, N'23Ю', 4)
INSERT [dbo].[Groups] ([Id], [Name], [Id_Status_group]) VALUES (70, N'19ИП1', 3)
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[Number_pair] ON 

INSERT [dbo].[Number_pair] ([Id], [Name]) VALUES (1, N'1')
INSERT [dbo].[Number_pair] ([Id], [Name]) VALUES (2, N'2')
INSERT [dbo].[Number_pair] ([Id], [Name]) VALUES (3, N'3')
INSERT [dbo].[Number_pair] ([Id], [Name]) VALUES (4, N'4')
INSERT [dbo].[Number_pair] ([Id], [Name]) VALUES (5, N'5')
INSERT [dbo].[Number_pair] ([Id], [Name]) VALUES (6, N'6')
INSERT [dbo].[Number_pair] ([Id], [Name]) VALUES (7, N'7')
SET IDENTITY_INSERT [dbo].[Number_pair] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Сотрудник учебного отдела')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Status_group] ON 

INSERT [dbo].[Status_group] ([Id], [Name]) VALUES (1, N'П/П')
INSERT [dbo].[Status_group] ([Id], [Name]) VALUES (2, N'У/П')
INSERT [dbo].[Status_group] ([Id], [Name]) VALUES (3, N'Сессия')
INSERT [dbo].[Status_group] ([Id], [Name]) VALUES (4, N'Обычный режим учебы')
SET IDENTITY_INSERT [dbo].[Status_group] OFF
GO
SET IDENTITY_INSERT [dbo].[Status_teacher] ON 

INSERT [dbo].[Status_teacher] ([Id], [Name]) VALUES (1, N'Б/Л')
INSERT [dbo].[Status_teacher] ([Id], [Name]) VALUES (2, N'У/П')
INSERT [dbo].[Status_teacher] ([Id], [Name]) VALUES (3, N'Командировка')
INSERT [dbo].[Status_teacher] ([Id], [Name]) VALUES (4, N'Обычный режим работы')
SET IDENTITY_INSERT [dbo].[Status_teacher] OFF
GO
SET IDENTITY_INSERT [dbo].[Subjects] ON 

INSERT [dbo].[Subjects] ([Id], [Name]) VALUES (1, N'Математика')
INSERT [dbo].[Subjects] ([Id], [Name]) VALUES (2, N'Технология Разработки')
INSERT [dbo].[Subjects] ([Id], [Name]) VALUES (3, N'Разработка Кода')
SET IDENTITY_INSERT [dbo].[Subjects] OFF
GO
SET IDENTITY_INSERT [dbo].[Teachers] ON 

INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (1, N'Абрамова', N'T.', N'В.', 1, 4, N'xpbtjnlb.wwj.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (2, N'Баранова', N'О.', N'И.', 1, 3, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (3, N'Воронкова', N'Л.', N'Б.', 1, 1, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (4, N'Голованов', N'А.', N'С.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (5, N'Глущенко', N'М.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (6, N'Данилова', N'Е.', N'А.', 1, 3, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (7, N'Дисюк', N'В.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (8, N'Долдина', N'Т.', N'В.', 1, 2, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (9, N'Домоседов', N'В.', N'И.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (10, N'Егоров', N'И.', N'В.', 1, 1, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (11, N'Елисеев', N'К.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (12, N'Жукова', N'Л.', N'Н.', 1, 3, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (13, N'Запруднов', N'И.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (14, N'Захарова', N'А.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (15, N'Захарова', N'Т.', N'Н.', 1, 1, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (16, N'Зимин', N'Ю.', N'М.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (17, N'Карелин', N'С.', N'С.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (18, N'Кирилова', N'М.', N'В.', 1, 1, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (19, N'Клюева', N'Е.', N'М.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (20, N'Коврова', N'А.', N'С.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (21, N'Комкова', N'Е.', N'М.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (22, N'Коновалова', N'Н.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (23, N'Кофанова', N'Н.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (24, N'Крутий-Филиппова', N'Е.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (25, N'Крылов', N'А.', N'С.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (26, N'Крылова', N'В.', N'И.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (27, N'Куксов', N'Е.', N'О.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (28, N'Кулдавлетова', N'Л.', N'Б.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (29, N'Кульпа', N'К.', N'Н.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (30, N'Лукомская', N'А.', N'Н.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (31, N'Малюгин', N'Д.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (32, N'Масленникова', N'Ю.', N'М.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (33, N'Маянцева', N'Ю.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (34, N'Недачина', N'И.', N'Д.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (35, N'Нефедов', N'А.', N'Г.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (36, N'Никитина', N'О.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (37, N'Овсяников', N'Е.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (38, N'Оленина', N'И.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (39, N'Павлова', N'Е.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (40, N'Панарский', N'Н.', N'С.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (41, N'Паншина', N'А.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (42, N'Перскевич', N'С.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (43, N'Петрова', N'Е.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (44, N'Пискарева', N'А.', N'Е.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (45, N'Простова', N'К.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (46, N'Прохорова', N'Ю.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (47, N'Рожкова', N'О.', N'Л.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (48, N'Савина', N'Е.', N'Н.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (49, N'Семенов', N'Н.', N'Е.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (50, N'Семенова', N'И.', N'К.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (51, N'Семенова', N'О.', N'Н.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (52, N'Скрипко', N'А.', N'Э.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (53, N'Смирнов', N'В.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (54, N'Смирнова', N'А.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (55, N'Смирнова', N'Е.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (56, N'Сорокина', N'Л.', N'С.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (57, N'Тараканов', N'А.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (58, N'Топенкова', N'Е.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (59, N'Трухина', N'А.', N'С.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (60, N'Федотова', N'К.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (61, N'Халезова', N'Е.', N'Т.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (62, N'Цыганкова', N'Н.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (63, N'Чернышева', N'А.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (64, N'Шестеркина', N'Е.', N'С.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (65, N'Шушунова', N'О.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (66, N'Щипков', N'С.', N'А.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (67, N'Чурочкин', N'С.', N'В.', 1, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (68, N'Ахмедова', N'Я.', N'Б.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (69, N'Бодрова', N'А.', N'А.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (70, N'Блинов', N'Е.', N'М.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (71, N'Гулая', N'Е.', N'В.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (72, N'Дидушенко', N'Е.', N'В.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (73, N'Захарова', N'В.', N'В.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (74, N'Козенкова', N'И.', N'И.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (75, N'Колодкина', N'И.', N'И.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (76, N'Кудрявцева', N'И.', N'С.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (77, N'Лапина', N'А.', N'О.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (78, N'Любимова', N'Н.', N'С.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (79, N'Магнитский', N'Р.', N'С.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (80, N'Меньшакова', N'И.', N'В.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (81, N'Паутов', N'А.', N'В.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (82, N'Пономарев', N'Е.', N'А.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (83, N'Реброва', N'О.', N'С.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (84, N'Савельев', N'В.', N'А.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (85, N'Сальников', N'А.', N'В.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (86, N'Сочилов', N'А.', N'Л.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (87, N'Стоволосова', N'И.', N'Б.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (88, N'Стрельникова', N'Т.', N'С.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (89, N'Халезов', N'Д.', N'А.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (90, N'Шамина', N'А.', N'А.', 2, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (91, N'Зборовская', N'Е.', N'Б.', 3, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (92, N'Тузкова', N'М.', N'В.', 3, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (93, N'Головлева', N'С.', N'М.', 4, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (94, N'Король', N'И.', N'И.', 4, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (95, N'Курзин', N'А.', N'А.', 4, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (96, N'Маланова', N'А.', N'Д.', 4, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (97, N'Митягина', N'Л.', N'С.', 4, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (98, N'Смирнова', N'А.', N'С.', 4, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (99, N'Травникова', N'Т.', N'Н.', 4, 4, N'teacher.png')
GO
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (100, N'Федорова', N'А.', N'В.', 4, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (101, N'Щербакова', N'А.', N'Б.', 4, 4, N'teacher.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (112, N'Kawazaki', N'k', N'k', 1, 1, N'iat2mkuk.z1v.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (113, N'Kago', N'k', N'k', 1, 1, N'0q020dxg.rax.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (114, N'Estripper', N'E', N'E', 1, 1, N'ocvdxrus.420.png')
INSERT [dbo].[Teachers] ([Id], [Family], [Name], [Patronymic], [Id_Type_employment], [Id_Status_teacher], [Image]) VALUES (115, N'Krico', N'F', N'F', 2, 2, N'ub2qxaai.4du.png')
SET IDENTITY_INSERT [dbo].[Teachers] OFF
GO
SET IDENTITY_INSERT [dbo].[Type_employment] ON 

INSERT [dbo].[Type_employment] ([Id], [Name]) VALUES (1, N'Штатный преподаватель')
INSERT [dbo].[Type_employment] ([Id], [Name]) VALUES (2, N'Штатный совместитель')
INSERT [dbo].[Type_employment] ([Id], [Name]) VALUES (3, N'Внешний совместитель')
INSERT [dbo].[Type_employment] ([Id], [Name]) VALUES (4, N'Совместитель с оплатой по часам')
SET IDENTITY_INSERT [dbo].[Type_employment] OFF
GO
ALTER TABLE [dbo].[Autorization]  WITH CHECK ADD  CONSTRAINT [FK_Autorization_Role] FOREIGN KEY([Id_Role])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Autorization] CHECK CONSTRAINT [FK_Autorization_Role]
GO
ALTER TABLE [dbo].[Changes]  WITH CHECK ADD  CONSTRAINT [FK_Changes_Cabinets] FOREIGN KEY([Id_Cabinets])
REFERENCES [dbo].[Cabinets] ([Id])
GO
ALTER TABLE [dbo].[Changes] CHECK CONSTRAINT [FK_Changes_Cabinets]
GO
ALTER TABLE [dbo].[Changes]  WITH CHECK ADD  CONSTRAINT [FK_Changes_Groups] FOREIGN KEY([Id_Group])
REFERENCES [dbo].[Groups] ([Id])
GO
ALTER TABLE [dbo].[Changes] CHECK CONSTRAINT [FK_Changes_Groups]
GO
ALTER TABLE [dbo].[Changes]  WITH CHECK ADD  CONSTRAINT [FK_Changes_Number_pair] FOREIGN KEY([Id_Number_pair])
REFERENCES [dbo].[Number_pair] ([Id])
GO
ALTER TABLE [dbo].[Changes] CHECK CONSTRAINT [FK_Changes_Number_pair]
GO
ALTER TABLE [dbo].[Changes]  WITH CHECK ADD  CONSTRAINT [FK_Changes_Subjects] FOREIGN KEY([Id_Subject])
REFERENCES [dbo].[Subjects] ([Id])
GO
ALTER TABLE [dbo].[Changes] CHECK CONSTRAINT [FK_Changes_Subjects]
GO
ALTER TABLE [dbo].[Changes]  WITH CHECK ADD  CONSTRAINT [FK_Changes_Teachers] FOREIGN KEY([Id_Teacher])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[Changes] CHECK CONSTRAINT [FK_Changes_Teachers]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Status_group] FOREIGN KEY([Id_Status_group])
REFERENCES [dbo].[Status_group] ([Id])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Status_group]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Cabinets] FOREIGN KEY([Id_Cabinets])
REFERENCES [dbo].[Cabinets] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Cabinets]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Day_week] FOREIGN KEY([Id_Day_week])
REFERENCES [dbo].[Day_week] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Day_week]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Groups] FOREIGN KEY([Id_Group])
REFERENCES [dbo].[Groups] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Groups]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Number_pair] FOREIGN KEY([Id_Number_pair])
REFERENCES [dbo].[Number_pair] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Number_pair]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Subjects] FOREIGN KEY([Id_Subject])
REFERENCES [dbo].[Subjects] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Subjects]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Teachers] FOREIGN KEY([Id_Teacher])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Teachers]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Status_teacher] FOREIGN KEY([Id_Status_teacher])
REFERENCES [dbo].[Status_teacher] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Status_teacher]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Type_employment] FOREIGN KEY([Id_Type_employment])
REFERENCES [dbo].[Type_employment] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Type_employment]
GO
USE [master]
GO
ALTER DATABASE [Study_department] SET  READ_WRITE 
GO
