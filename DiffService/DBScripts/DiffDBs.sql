/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2016
    Target Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [master]
GO

/****** Object:  Database [diffdb]    Script Date: 8/27/2017 6:46:45 PM ******/
CREATE DATABASE [diffdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'diffdb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\diffdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'diffdb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\diffdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [diffdb] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [diffdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [diffdb] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [diffdb] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [diffdb] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [diffdb] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [diffdb] SET ARITHABORT OFF 
GO

ALTER DATABASE [diffdb] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [diffdb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [diffdb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [diffdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [diffdb] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [diffdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [diffdb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [diffdb] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [diffdb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [diffdb] SET  DISABLE_BROKER 
GO

ALTER DATABASE [diffdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [diffdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [diffdb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [diffdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [diffdb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [diffdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [diffdb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [diffdb] SET RECOVERY FULL 
GO

ALTER DATABASE [diffdb] SET  MULTI_USER 
GO

ALTER DATABASE [diffdb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [diffdb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [diffdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [diffdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [diffdb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [diffdb] SET QUERY_STORE = OFF
GO

USE [diffdb]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [diffdb] SET  READ_WRITE 
GO

/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2016
    Target Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [master]
GO

/****** Object:  Database [diffdb_UnitTest]    Script Date: 8/27/2017 6:47:28 PM ******/
CREATE DATABASE [diffdb_UnitTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'diffdb_UnitTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\diffdb_UnitTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'diffdb_UnitTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\diffdb_UnitTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [diffdb_UnitTest] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [diffdb_UnitTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [diffdb_UnitTest] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET ARITHABORT OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [diffdb_UnitTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [diffdb_UnitTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET  DISABLE_BROKER 
GO

ALTER DATABASE [diffdb_UnitTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [diffdb_UnitTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET RECOVERY FULL 
GO

ALTER DATABASE [diffdb_UnitTest] SET  MULTI_USER 
GO

ALTER DATABASE [diffdb_UnitTest] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [diffdb_UnitTest] SET DB_CHAINING OFF 
GO

ALTER DATABASE [diffdb_UnitTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [diffdb_UnitTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [diffdb_UnitTest] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [diffdb_UnitTest] SET QUERY_STORE = OFF
GO

USE [diffdb_UnitTest]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [diffdb_UnitTest] SET  READ_WRITE 
GO

USE [diffdb]
GO
/****** Object:  Table [dbo].[Comparison]    Script Date: 8/27/2017 6:46:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comparison](
	[ComparisonId] [varchar](50) NOT NULL,
	[Left] [varchar](max) NULL,
	[Right] [varchar](max) NULL,
 CONSTRAINT [PK_Comparisons] PRIMARY KEY CLUSTERED 
(
	[ComparisonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

USE [diffdb_UnitTest]
GO
/****** Object:  Table [dbo].[Comparison]    Script Date: 8/27/2017 6:46:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comparison](
	[ComparisonId] [varchar](50) NOT NULL,
	[Left] [varchar](max) NULL,
	[Right] [varchar](max) NULL,
 CONSTRAINT [PK_Comparisons] PRIMARY KEY CLUSTERED 
(
	[ComparisonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

