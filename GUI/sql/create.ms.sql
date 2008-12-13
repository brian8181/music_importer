USE [master]
GO
/****** Object:  Database [music]    Script Date: 12/13/2008 10:37:59 ******/
CREATE DATABASE [music] ON  PRIMARY 
( NAME = N'music', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\music.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'music_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\music_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'music', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [music].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [music] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [music] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [music] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [music] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [music] SET ARITHABORT OFF 
GO
ALTER DATABASE [music] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [music] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [music] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [music] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [music] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [music] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [music] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [music] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [music] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [music] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [music] SET  DISABLE_BROKER 
GO
ALTER DATABASE [music] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [music] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [music] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [music] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [music] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [music] SET  READ_WRITE 
GO
ALTER DATABASE [music] SET RECOVERY FULL 
GO
ALTER DATABASE [music] SET  MULTI_USER 
GO
ALTER DATABASE [music] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [music] SET DB_CHAINING OFF 

USE [music]
GO
/****** Object:  Table [dbo].[album]    Script Date: 12/13/2008 10:39:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[album](
	[id] [int] IDENTITY(1,3) NOT NULL,
	[album] [varchar](50) NOT NULL,
	[artist] [varchar](50) NULL,
	[art] [nchar](10) NULL,
	[extra] [varchar](50) NULL,
	[update_ts] [datetime] NULL,
	[insert_ts] [timestamp] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [music]
GO
/****** Object:  Table [dbo].[art]    Script Date: 12/13/2008 10:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[art](
	[id] [int] NULL,
	[file] [varbinary](256) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [music]
GO
/****** Object:  Table [dbo].[artist]    Script Date: 12/13/2008 10:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[artist](
	[id] [int] NOT NULL,
	[artist] [varchar](50) NOT NULL,
	[extra] [varchar](max) NULL,
	[update_ts] [datetime] NULL,
	[insert_ts] [timestamp] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [music]
GO
/****** Object:  Table [dbo].[song]    Script Date: 12/13/2008 10:41:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[song](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[album_id] [int] NULL,
	[artist_id] [int] NULL,
	[track] [int] NULL,
	[title] [varchar](50) NULL,
	[file] [varchar](50) NULL,
	[genre] [varchar](50) NULL,
	[bitrate] [int] NULL,
	[length] [varchar](50) NULL,
	[year] [int] NULL,
	[comments] [varchar](max) NULL,
	[encoder] [varchar](50) NULL,
	[file_size] [int] NULL,
	[file_type] [varbinary](50) NULL,
	[art_id] [int] NULL,
	[lyrics] [varbinary](max) NULL,
	[composer] [varchar](50) NULL,
	[conductor] [varchar](50) NULL,
	[copyright] [varchar](50) NULL,
	[disc] [int] NULL,
	[disc_count] [int] NULL,
	[performer] [varchar](50) NULL,
	[tag_types] [varchar](50) NULL,
	[beats_per_minute] [int] NULL,
	[update_ts] [datetime] NULL,
	[insert_ts] [timestamp] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

USE [music]
GO
/****** Object:  Table [dbo].[update]    Script Date: 12/13/2008 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[update](
	[id] [int] NULL,
	[update] [varbinary](50) NULL,
	[version] [int] NULL,
	[release_date] [datetime] NULL,
	[insert_ts] [timestamp] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

