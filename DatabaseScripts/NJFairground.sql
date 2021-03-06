USE [master]
GO
/****** Object:  Database [NJFairgroundDB]    Script Date: 5/25/2014 5:30:12 PM ******/
CREATE DATABASE [NJFairgroundDB] ON  PRIMARY 
( NAME = N'NJFairgroundDB', FILENAME = N'K:\PROJECT REPOSITORY\NJFairground\NJFairgroundDB\NJFairgroundDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NJFairgroundDB_log', FILENAME = N'K:\PROJECT REPOSITORY\NJFairground\NJFairgroundDB\NJFairgroundDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NJFairgroundDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NJFairgroundDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [NJFairgroundDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NJFairgroundDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NJFairgroundDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NJFairgroundDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NJFairgroundDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NJFairgroundDB] SET RECOVERY FULL 
GO
ALTER DATABASE [NJFairgroundDB] SET  MULTI_USER 
GO
ALTER DATABASE [NJFairgroundDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NJFairgroundDB] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'NJFairgroundDB', N'ON'
GO
USE [NJFairgroundDB]
GO
/****** Object:  Schema [master]    Script Date: 5/25/2014 5:30:12 PM ******/
CREATE SCHEMA [master]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 5/25/2014 5:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event](
	[EventId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[PageItemId] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[EventTitle] [varchar](100) NOT NULL,
	[EventDesc] [nvarchar](1000) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Page]    Script Date: 5/25/2014 5:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Page](
	[PageId] [int] NOT NULL,
	[PageName] [varchar](50) NOT NULL,
	[PageDesc] [nvarchar](50) NULL,
	[StatusId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PageHeader]    Script Date: 5/25/2014 5:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PageHeader](
	[PageHeaderId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[PageItemId] [int] NOT NULL,
	[PageHeaderText] [varchar](50) NULL,
	[PageSubHeaderText] [varchar](50) NULL,
	[StatusId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_PageHeader] PRIMARY KEY CLUSTERED 
(
	[PageHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PageItem]    Script Date: 5/25/2014 5:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PageItem](
	[PageItemId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[PageItemType] [int] NOT NULL,
	[PageItemImage] [varchar](200) NULL,
	[StatusId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ActivatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_PageItem] PRIMARY KEY CLUSTERED 
(
	[PageItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PageItemDetail]    Script Date: 5/25/2014 5:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageItemDetail](
	[PageItemDetailId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[PageItemId] [int] NOT NULL,
	[PageItemDetailText] [nvarchar](2000) NOT NULL,
	[PageItemSubDetail] [nvarchar](1000) NULL,
	[StatusId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_PageItemDetail] PRIMARY KEY CLUSTERED 
(
	[PageItemDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (1, N'Page1', N'Page1', 1)
GO
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (2, N'Page2', N'Page2', 1)
GO
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (3, N'Page3', N'Page3', 1)
GO
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (5, N'Page5', N'Page6', 1)
GO
ALTER TABLE [dbo].[PageItem] ADD  CONSTRAINT [DF_PageItem_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[PageItem] ADD  CONSTRAINT [DF_PageItem_ActivatedOn]  DEFAULT (getdate()) FOR [ActivatedOn]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Page]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_PageItem]
GO
ALTER TABLE [dbo].[PageHeader]  WITH CHECK ADD  CONSTRAINT [FK_PageHeader_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageHeader] CHECK CONSTRAINT [FK_PageHeader_Page]
GO
ALTER TABLE [dbo].[PageHeader]  WITH CHECK ADD  CONSTRAINT [FK_PageHeader_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[PageHeader] CHECK CONSTRAINT [FK_PageHeader_PageItem]
GO
ALTER TABLE [dbo].[PageItem]  WITH CHECK ADD  CONSTRAINT [FK_PageItem_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageItem] CHECK CONSTRAINT [FK_PageItem_Page]
GO
ALTER TABLE [dbo].[PageItemDetail]  WITH CHECK ADD  CONSTRAINT [FK_PageItemDetail_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageItemDetail] CHECK CONSTRAINT [FK_PageItemDetail_Page]
GO
ALTER TABLE [dbo].[PageItemDetail]  WITH CHECK ADD  CONSTRAINT [FK_PageItemDetail_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[PageItemDetail] CHECK CONSTRAINT [FK_PageItemDetail_PageItem]
GO
USE [master]
GO
ALTER DATABASE [NJFairgroundDB] SET  READ_WRITE 
GO
