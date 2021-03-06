USE [master]
GO
/****** Object:  Database [dbGenericConfig]    Script Date: 7/5/2022 1:14:54 PM ******/
CREATE DATABASE [dbGenericConfig]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbGenericConfig', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\dbGenericConfig.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbGenericConfig_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\dbGenericConfig_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbGenericConfig] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbGenericConfig].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbGenericConfig] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbGenericConfig] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbGenericConfig] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbGenericConfig] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbGenericConfig] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbGenericConfig] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbGenericConfig] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbGenericConfig] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbGenericConfig] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbGenericConfig] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbGenericConfig] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbGenericConfig] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbGenericConfig] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbGenericConfig] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbGenericConfig] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbGenericConfig] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbGenericConfig] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbGenericConfig] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbGenericConfig] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbGenericConfig] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbGenericConfig] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbGenericConfig] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbGenericConfig] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbGenericConfig] SET  MULTI_USER 
GO
ALTER DATABASE [dbGenericConfig] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbGenericConfig] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbGenericConfig] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbGenericConfig] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbGenericConfig] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbGenericConfig] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [dbGenericConfig] SET QUERY_STORE = OFF
GO
USE [dbGenericConfig]
GO
/****** Object:  User [gConfig]    Script Date: 7/5/2022 1:14:54 PM ******/
CREATE USER [gConfig] FOR LOGIN [gConfig] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [gConfig]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 7/5/2022 1:14:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
	[IsActive] [int] NOT NULL,
	[ApplicationName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [dbGenericConfig] SET  READ_WRITE 
GO


USE [dbGenericConfig]
GO

INSERT INTO [dbo].[Config]
           ([Id]
           ,[Name]
           ,[Type]
           ,[Value]
           ,[IsActive]
           ,[ApplicationName])
     VALUES
           ('1', 'SiteName', 'STRING', 'allyouplay.com1', 1, 'SERVICE-A')
GO

