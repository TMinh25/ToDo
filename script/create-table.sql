USE [MicrosoftToDo]
GO
/****** Object:  Table [dbo].[User]    Script Date: 30/05/2021 22:02:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 30/05/2021 22:02:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
DROP TABLE [dbo].[Task]
GO
/****** Object:  Table [dbo].[CustomMenu]    Script Date: 30/05/2021 22:02:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomMenu]') AND type in (N'U'))
DROP TABLE [dbo].[CustomMenu]
GO
/****** Object:  Table [dbo].[Avatar]    Script Date: 30/05/2021 22:02:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Avatar]') AND type in (N'U'))
DROP TABLE [dbo].[Avatar]
GO
USE [master]
GO
/****** Object:  Database [MicrosoftToDo]    Script Date: 30/05/2021 22:02:11 ******/
DROP DATABASE [MicrosoftToDo]
GO
/****** Object:  Database [MicrosoftToDo]    Script Date: 30/05/2021 22:02:11 ******/
CREATE DATABASE [MicrosoftToDo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MicrosoftToDo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MicrosoftToDo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MicrosoftToDo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MicrosoftToDo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MicrosoftToDo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MicrosoftToDo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MicrosoftToDo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET ARITHABORT OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MicrosoftToDo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MicrosoftToDo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MicrosoftToDo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MicrosoftToDo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET RECOVERY FULL 
GO
ALTER DATABASE [MicrosoftToDo] SET  MULTI_USER 
GO
ALTER DATABASE [MicrosoftToDo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MicrosoftToDo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MicrosoftToDo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MicrosoftToDo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MicrosoftToDo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MicrosoftToDo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MicrosoftToDo', N'ON'
GO
ALTER DATABASE [MicrosoftToDo] SET QUERY_STORE = OFF
GO
USE [MicrosoftToDo]
GO
/****** Object:  Table [dbo].[Avatar]    Script Date: 30/05/2021 22:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avatar](
	[id_avatar] [int] IDENTITY(1,1) NOT NULL,
	[data] [image] NOT NULL,
	[fileName] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Avatar] PRIMARY KEY CLUSTERED 
(
	[id_avatar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomMenu]    Script Date: 30/05/2021 22:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomMenu](
	[id_customMenu] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[title] [nvarchar](20) NOT NULL,
	[iconFont] [nvarchar](10) NULL,
	[backColor] [nvarchar](7) NOT NULL,
 CONSTRAINT [PK_CustomMenu] PRIMARY KEY CLUSTERED 
(
	[id_customMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 30/05/2021 22:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[id_task] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[taskContent] [nvarchar](50) NOT NULL,
	[timeCreated] [datetime] NULL,
	[category] [nvarchar](50) NULL,
	[status] [bit] NOT NULL,
	[isImportant] [bit] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[id_task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 30/05/2021 22:02:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[displayName] [nchar](30) NOT NULL,
	[id_avatar] [int] NOT NULL,
	[username] [varchar](30) NOT NULL,
	[password] [nchar](16) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [MicrosoftToDo] SET  READ_WRITE 
GO
