USE [master]
GO

CREATE DATABASE [PROYECTO_FARMACIA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PROYECTO_FARMACIA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PROYECTO_FARMACIA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PROYECTO_FARMACIA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PROYECTO_FARMACIA.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PROYECTO_FARMACIA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET ARITHABORT OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET RECOVERY FULL 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET  MULTI_USER 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET QUERY_STORE = OFF
GO

ALTER DATABASE [PROYECTO_FARMACIA] SET  READ_WRITE 
GO
