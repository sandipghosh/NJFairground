USE [NJFairgroundDB]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 06/07/2014 23:38:03 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (1, N'Page1', N'Page1', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (2, N'Page2', N'Page2', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (3, N'Page3', N'Page3', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (4, N'Event', N'Event', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (5, N'HorseShow', N'Horse Show', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (6, N'AGLearningCenter', N'Agriculture/AG Learning Center', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (7, N'ConservatoryAndCourtyard', N'Conservatory & Courtyard', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (9, N'Promote', N'Promote Your Events Here!', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (11, N'WhatsNew', N'WhatsNew', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (12, N'Info', N'Info', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (14, N'DailyHighlights', N'Daily Highlights', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (15, N'Fun', N'Fun', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (16, N'Food', N'Food', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (17, N'Shopping', N'Shopping', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (18, N'Social', N'Social', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (19, N'Complex', N'Fairgrounds Complex All Season/Events/Rentals', 1)
/****** Object:  Table [dbo].[PageItem]    Script Date: 06/07/2014 23:38:03 ******/
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
	[PageHeaderText] [varchar](50) NOT NULL,
	[PageSubHeaderText] [varchar](100) NULL,
	[PageItemImage] [varchar](200) NULL,
	[PageItemDetailText] [varchar](2000) NOT NULL,
	[PageItemSubDetail] [varchar](1000) NULL,
	[StatusId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ActivatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_PageItem] PRIMARY KEY CLUSTERED 
(
	[PageItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (501, 5, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'horseshow-pic.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (502, 5, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'horseshow-pic.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (503, 5, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'horseshow-pic.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (601, 6, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'schedule-pic1.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (602, 6, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'schedule-pic1.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (603, 6, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'schedule-pic1.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (604, 6, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'schedule-pic1.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (605, 6, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', NULL, N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (606, 6, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', NULL, N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 0, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (701, 7, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (702, 7, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (703, 7, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (704, 7, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (901, 9, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (902, 9, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (903, 9, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (904, 9, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1101, 11, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1102, 11, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1103, 11, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1104, 11, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1201, 12, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'info-pic1.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1202, 12, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'info-pic1.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1203, 12, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'info-pic1.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1204, 12, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'info-pic1.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1401, 14, 100, N'August 1', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1402, 14, 100, N'August 2', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1403, 14, 100, N'August 3', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1404, 14, 100, N'August 4', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1405, 14, 100, N'August 5', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1501, 15, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1502, 15, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1503, 15, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1601, 16, 100, N'2014 Vendor Application', N'Applications for the 2014 Fair are now', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1602, 16, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1603, 16, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1604, 16, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1701, 17, 100, N'Lorem Ipsum', N'2014 Vendor Application', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1702, 17, 100, N'Lorem Ipsum', N'2014 Vendor Application', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1703, 17, 100, N'Lorem Ipsum', N'2014 Vendor Application', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1704, 17, 100, N'Lorem Ipsum', N'2014 Vendor Application', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1801, 18, 100, N'Facebook', N'Lorem ipsum dumy text Lorem', N'facebook.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1802, 18, 100, N'Twiiter', N'Lorem ipsum dumy text Lorem', N'twitter.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1803, 18, 100, N'YouTube', N'Lorem ipsum dumy text Lorem', N'youtube.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1901, 19, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1902, 19, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1903, 19, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1904, 19, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1905, 19, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
/****** Object:  Table [dbo].[PageItemDetail]    Script Date: 06/07/2014 23:38:03 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[PageItemDetail] ([PageItemDetailId], [PageId], [PageItemId], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn]) VALUES (12001, 12, 1201, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', 1, CAST(0x0000A30500000000 AS DateTime))
INSERT [dbo].[PageItemDetail] ([PageItemDetailId], [PageId], [PageItemId], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn]) VALUES (12002, 12, 1202, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', 1, CAST(0x0000A30500000000 AS DateTime))
/****** Object:  Table [dbo].[PageHeader]    Script Date: 06/07/2014 23:38:03 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[PageHeader] ([PageHeaderId], [PageId], [PageItemId], [PageHeaderText], [PageSubHeaderText], [StatusId]) VALUES (1, 12, 1201, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', 1)
INSERT [dbo].[PageHeader] ([PageHeaderId], [PageId], [PageItemId], [PageHeaderText], [PageSubHeaderText], [StatusId]) VALUES (2, 12, 1202, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', 1)
INSERT [dbo].[PageHeader] ([PageHeaderId], [PageId], [PageItemId], [PageHeaderText], [PageSubHeaderText], [StatusId]) VALUES (3, 12, 1203, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', 1)
INSERT [dbo].[PageHeader] ([PageHeaderId], [PageId], [PageItemId], [PageHeaderText], [PageSubHeaderText], [StatusId]) VALUES (4, 12, 1204, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', 1)
/****** Object:  Table [dbo].[Event]    Script Date: 06/07/2014 23:38:03 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_PageItem_CreatedOn]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[PageItem] ADD  CONSTRAINT [DF_PageItem_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  Default [DF_PageItem_ActivatedOn]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[PageItem] ADD  CONSTRAINT [DF_PageItem_ActivatedOn]  DEFAULT (getdate()) FOR [ActivatedOn]
GO
/****** Object:  ForeignKey [FK_Event_Page]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Page]
GO
/****** Object:  ForeignKey [FK_Event_PageItem]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_PageItem]
GO
/****** Object:  ForeignKey [FK_PageHeader_Page]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[PageHeader]  WITH CHECK ADD  CONSTRAINT [FK_PageHeader_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageHeader] CHECK CONSTRAINT [FK_PageHeader_Page]
GO
/****** Object:  ForeignKey [FK_PageHeader_PageItem]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[PageHeader]  WITH CHECK ADD  CONSTRAINT [FK_PageHeader_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[PageHeader] CHECK CONSTRAINT [FK_PageHeader_PageItem]
GO
/****** Object:  ForeignKey [FK_PageItem_Page]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[PageItem]  WITH CHECK ADD  CONSTRAINT [FK_PageItem_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageItem] CHECK CONSTRAINT [FK_PageItem_Page]
GO
/****** Object:  ForeignKey [FK_PageItemDetail_Page]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[PageItemDetail]  WITH CHECK ADD  CONSTRAINT [FK_PageItemDetail_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageItemDetail] CHECK CONSTRAINT [FK_PageItemDetail_Page]
GO
/****** Object:  ForeignKey [FK_PageItemDetail_PageItem]    Script Date: 06/07/2014 23:38:03 ******/
ALTER TABLE [dbo].[PageItemDetail]  WITH CHECK ADD  CONSTRAINT [FK_PageItemDetail_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[PageItemDetail] CHECK CONSTRAINT [FK_PageItemDetail_PageItem]
GO
