USE [master]
GO
/****** Object:  Database [music]    Script Date: 09/09/2008 13:18:14 ******/
CREATE DATABASE [music] ON  PRIMARY 
( NAME = N'music', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\music.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'music_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\music_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
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
GO
USE [music]
GO
/****** Object:  Table [dbo].[album]    Script Date: 09/09/2008 13:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[album](
	[id] [int] IDENTITY(1,3) NOT NULL,
	[album] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[artist] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[art] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,	
	[extra] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[update_ts] [timestamp] NULL,
	[insert_ts] [datetime] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[art](
	[id] [int] NOT NULL,
	[hash] [blob] NOT NULL, 
	[update_ts] [timestamp] NULL,
	[insert_ts] [datetime] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[playlists](
	[id] [int] NOT NULL,
	[update_ts] [timestamp] NULL,
	[insert_ts] [datetime] NULL
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[playlist_songs](
	[id] [int] NOT NULL,
	[update_ts] [timestamp] NULL,
	[insert_ts] [datetime] NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[artist]    Script Date: 09/09/2008 13:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[artist](
	[id] [int] NOT NULL,
	[artist] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[extra] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[update_ts] [timestamp] NULL,
	[insert_ts] [datetime] NULL
) ON [PRIMARY]

GO
USE [music]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[song](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[artist_id] [int] NULL,
	[album_id] [int] NULL,
	[title] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[track] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[file] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[genre] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[bitrate] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[length] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[year] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[comments] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[encoder] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[file_size] [int] NULL,
	[file_type] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[art_id] [int] NULL,
	[lyrics] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[composer] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conductor] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[copyright] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[disc] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[disc_count] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[performer] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[tag_types] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[track_count] [int] NULL,
	[beats_per_minute] [int] NULL
 	[update_ts] [timestamp] NULL,
	[insert_ts] [datetime] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[query_log](
	[id] [int] NOT NULL,
	[update_ts] [timestamp] NULL,
	[insert_ts] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF