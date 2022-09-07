USE [master]
GO
/****** Object:  Database [WM]    Script Date: 9/7/2022 12:33:19 AM ******/
CREATE DATABASE [WM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\WM.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\WM_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WM] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WM] SET ARITHABORT OFF 
GO
ALTER DATABASE [WM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WM] SET RECOVERY FULL 
GO
ALTER DATABASE [WM] SET  MULTI_USER 
GO
ALTER DATABASE [WM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WM] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'WM', N'ON'
GO
ALTER DATABASE [WM] SET QUERY_STORE = OFF
GO
USE [WM]

GO
/****** Object:  Table [dbo].[GlobalUniqueIdentifier]    Script Date: 9/7/2022 12:33:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalUniqueIdentifier](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[guid] [nvarchar](50) NOT NULL,
	[expire] [date] NOT NULL,
	[usr] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_GlobalUniqueIdentifier] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [WM] SET  READ_WRITE 
GO