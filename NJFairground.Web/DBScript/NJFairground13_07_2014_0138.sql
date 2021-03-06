USE [NJFairgroundDB]
GO
/****** Object:  ForeignKey [FK_Event_Page]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Page]
GO
/****** Object:  ForeignKey [FK_Event_PageItem]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_PageItem]
GO
/****** Object:  ForeignKey [FK_PageHeader_Page]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageHeader] DROP CONSTRAINT [FK_PageHeader_Page]
GO
/****** Object:  ForeignKey [FK_PageHeader_PageItem]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageHeader] DROP CONSTRAINT [FK_PageHeader_PageItem]
GO
/****** Object:  ForeignKey [FK_PageItem_Page]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageItem] DROP CONSTRAINT [FK_PageItem_Page]
GO
/****** Object:  ForeignKey [FK_PageItemDetail_Page]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageItemDetail] DROP CONSTRAINT [FK_PageItemDetail_Page]
GO
/****** Object:  ForeignKey [FK_PageItemDetail_PageItem]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageItemDetail] DROP CONSTRAINT [FK_PageItemDetail_PageItem]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Page]
GO
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_PageItem]
GO
DROP TABLE [dbo].[Event]
GO
/****** Object:  Table [dbo].[PageHeader]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageHeader] DROP CONSTRAINT [FK_PageHeader_Page]
GO
ALTER TABLE [dbo].[PageHeader] DROP CONSTRAINT [FK_PageHeader_PageItem]
GO
DROP TABLE [dbo].[PageHeader]
GO
/****** Object:  Table [dbo].[PageItemDetail]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageItemDetail] DROP CONSTRAINT [FK_PageItemDetail_Page]
GO
ALTER TABLE [dbo].[PageItemDetail] DROP CONSTRAINT [FK_PageItemDetail_PageItem]
GO
DROP TABLE [dbo].[PageItemDetail]
GO
/****** Object:  Table [dbo].[PageItem]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageItem] DROP CONSTRAINT [FK_PageItem_Page]
GO
ALTER TABLE [dbo].[PageItem] DROP CONSTRAINT [DF_PageItem_CreatedOn]
GO
ALTER TABLE [dbo].[PageItem] DROP CONSTRAINT [DF_PageItem_ActivatedOn]
GO
DROP TABLE [dbo].[PageItem]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 07/13/2014 01:36:48 ******/
DROP TABLE [dbo].[Page]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 07/13/2014 01:36:48 ******/
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
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (10, N'Directions', N'Directions', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (11, N'WhatsNew', N'WhatsNew', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (12, N'Info', N'Info', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (14, N'DailyHighlights', N'Daily Highlights', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (15, N'Fun', N'Fun', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (16, N'Food', N'Food', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (17, N'Shopping', N'Shopping', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (18, N'Social', N'Social', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (19, N'Complex', N'Fairgrounds Complex All Season/Events/Rentals', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (20, N'DirectionToFairground', N'DirectionToFairground', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (21, N'Carnival', N'The Carnival', 1)
INSERT [dbo].[Page] ([PageId], [PageName], [PageDesc], [StatusId]) VALUES (22, N'CircusHollywood', N'Circus Hollywood', 1)
/****** Object:  Table [dbo].[PageItem]    Script Date: 07/13/2014 01:36:48 ******/
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
	[PageHeaderText] [varchar](1000) NOT NULL,
	[PageSubHeaderText] [varchar](1000) NULL,
	[PageItemImage] [varchar](200) NULL,
	[PageItemDetailText] [varchar](max) NOT NULL,
	[PageItemSubDetail] [varchar](1000) NULL,
	[StatusId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_PageItem_CreatedOn]  DEFAULT (getdate()),
	[ActivatedOn] [datetime] NOT NULL CONSTRAINT [DF_PageItem_ActivatedOn]  DEFAULT (getdate()),
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
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (501, 5, 100, N'Horse Show', N'Horse Show', N'horseshow-pic.jpg', N'<p>Horses have been a big part of the Sussex County fair since 1923! The Sussex County Horse Show demonstrates the excellence and commitment of the community to these wonderful animals and the traditional event. This year’s show includes hunters, jumpers, quarter horse, and more, with over 2,000 participants!</p><br><p>Three rings alternate shows beginning with quarter horses Aug. 1-3, and followed by a variety of classes of the Sussex County Horse Show which pulls riders from a number of states.</p><br><p>Find your favorite events or check out new ones! Shows run all four days of The Fair.</p>', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (502, 5, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'horseshow-pic.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (503, 5, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'horseshow-pic.jpg', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (601, 6, 100, N'Richards Educational Building', N'Enjoy an art show, photography show, creative arts for home and hobby demonstration, The Fair history exhibit, Grange exhibit & honey show.', N'schedule-pic1.jpg', N'Richards Educational Building: Enjoy an art show, photography show, creative arts for home and hobby demonstration, The Fair history exhibit, Grange exhibit & honey show.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (602, 6, 100, N'Shotwell 4-H Building', N'Visit 4-H exhibits and 4-H club booths with daily presentations.', N'schedule-pic1.jpg', N'Shotwell 4-H Building: Visit 4-H exhibits and 4-H club booths with daily presentations.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (603, 6, 100, N'Sussex County Building ', N' Find more information information about the municipalities and services in Sussex County.', N'schedule-pic1.jpg', N'Sussex County Building: Find more information information about the municipalities and services in Sussex County.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (604, 6, 100, N'Conservatory Courtyard Tent', N'Daily scheduled events such including Vet Talks and Country Line Dancing.', N'schedule-pic1.jpg', N'Conservatory Courtyard Tent: Daily scheduled events such including Vet Talks and Country Line Dancing.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (605, 6, 100, N'Daily Tractor Parade', N'View the tractors that help make Sussex County Agriculture possible!', N'schedule-pic1.jpg', N'Daily Tractor Parade: View the tractors that help make Sussex County Agriculture possible!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (606, 6, 100, N'Animal of the Day', N'Our featured animals are sure to entertain and educate!', N'schedule-pic1.jpg', N'Animal of the Day:  Our featured animals are sure to entertain and educate!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (607, 6, 100, N'Rare Breed Education Tent', N'Find out about some of the rarest breeds in the agricultural field!', N'schedule-pic1.jpg', N'Rare Breed Education Tent :Find out about some of the rarest breeds in the agricultural field!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (608, 6, 100, N'Snook Memorial Museum', N'Find out about the history of The Sussex County Farm & Horse Show and visit an old-time farm kitchen and general store with antique farm equipment.', N'schedule-pic1.jpg', N'Snook Memorial Museum: Find out about the history of The Sussex County Farm & Horse Show and visit an old-time farm kitchen and general store with antique farm equipment.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (609, 6, 100, N'Farmers’ Market', N'Enjoy the freshest fruits, vegetables, flowers, jams and more from your local farmers!', N'schedule-pic1.jpg', N'Farmers’ Market: Enjoy the freshest fruits, vegetables, flowers, jams and more from your local farmers!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (610, 6, 100, N'Antique Engines', N'Vehicle enthusiasts and antique collectors will be amazed by these engines of yesterday!', N'schedule-pic1.jpg', N'Antique Engines: Vehicle enthusiasts and antique collectors will be amazed by these engines of yesterday!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (701, 7, 100, N'Wedding Events:', N'Engagement Parties, Bachelor/Bachelorette Parties, Showers, Ceremonies, Receptions, Cocktail Hours, Silver & Gold Anniversaries', N'star.png', N'Wedding Events: Engagement Parties, Bachelor/Bachelorette Parties, Showers, Ceremonies, Receptions, Cocktail Hours, Silver & Gold Anniversaries', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (702, 7, 100, N'Social Events:', N'Bar/Bat Mitzvahs, Graduation Parties, Family Reunions, Baby Showers, Milestone Birthdays, Going Away/Retirement Parties, Memorials and Life Celebrations, High School Proms, and more!', N'star.png', N'Social Events: Bar/Bat Mitzvahs, Graduation Parties, Family Reunions, Baby Showers, Milestone Birthdays, Going Away/Retirement Parties, Memorials and Life Celebrations, High School Proms, and more!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (703, 7, 100, N'Corporate Events: ', N' Professional Conferences, Workshops, Trainings, Corporate Luncheons, Company Parties, Presentations, Corporate Entertainment, Job Showcases, and Fundraising Events.', N'star.png', N'Corporate Events: Professional Conferences, Workshops, Trainings, Corporate Luncheons, Company Parties, Presentations, Corporate Entertainment, Job Showcases, and Fundraising Events.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (901, 9, 100, N'Promote Your Events Here!', N'Promote Your Events Here!', N'banner-promote.jpg', N'<p>Put the Power of the New Jersey State Fair to Work for Your Business!</p><br><p>Our mobile app sponsors enjoy the media and advertising benefits of sponsorship, as well as a variety of other benefits that may include (depending on the level of sponsorship):</p><br><p>• Features on our mobile app and interactive app<br />• Mobile banner ads<br />• Social media recognition and partner promotion<br />• Admission passes<br />• VIP parking<br />• Logo and link to your website from the New Jersey State Fair mobile app<br />• Inclusion in the online exhibitor handbook as event sponsor<br />• Demolition Derby tickets</p><br><p>For information about sponsorship opportunities at the New Jersey State Fair&#174; Contact : Kristin Vincent, Sponsorship &amp; Events Manager<br />Telephone: 973-948-5500 x258<br />Email: kvincent@njstatefair.org</p>', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (902, 9, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (903, 9, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (904, 9, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1001, 10, 100, N'•	From Rt. 84 (NY & PA):', N' Take Milford exit, Route 6 East to Route 209 South. Cross bridge. Route 206 South 10 miles to Plains Road. Turn left at light.', NULL, N'•	From Rt. 84 (NY & PA): Take Milford exit, Route 6 East to Route 209 South. Cross bridge. Route 206 South 10 miles to Plains Road. Turn left at light.', NULL, 1, CAST(0x0000A35401866354 AS DateTime), CAST(0x0000A35401866354 AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1002, 10, 100, N'•	From Rt. 80: ', N'Take exit 34B, Route 15 North to Route 206 North, 1 mile to Plains Road, turn right at light.', NULL, N'•	From Rt. 80: Take exit 34B, Route 15 North to Route 206 North, 1 mile to Plains Road, turn right at light.', NULL, 1, CAST(0x0000A35401866354 AS DateTime), CAST(0x0000A35401866354 AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1003, 10, 100, N'•	From Rt. 23:', N' Route 23 North to Sussex Boro. Take Route 639, then Route 565 to right on Plains Road, or right on Linn Smith, left on Plains Road.', NULL, N'•	From Rt. 23: Route 23 North to Sussex Boro. Take Route 639, then Route 565 to right on Plains Road, or right on Linn Smith, left on Plains Road.', NULL, 1, CAST(0x0000A35401866354 AS DateTime), CAST(0x0000A35401866354 AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1101, 11, 100, N'The 2014 Fair will debut Circus Hollywood—', N'a real circus, free of charge with admission. See animals, racing pigs, a petting zoo, and several free shows daily under the Big Top.', N'What''s New 1.jpg', N'The 2014 Fair will debut Circus Hollywood—a real circus, free of charge with admission. See animals, racing pigs, a petting zoo, and several free shows daily under the Big Top.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1102, 11, 100, N'The Agricultural Area ', N'Is debuting Redneck Wrestling! Sussex County is known for its outstanding wrestling programs for over 60 years.', N'What''s New 2.jpg', N'The Agricultural Area is debuting Redneck Wrestling! Sussex County is known for its outstanding wrestling programs for over 60 years.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1103, 11, 100, N'The Carnival ', N'Is debuting new and spruced up rides with sensational lighting displays.', N'What''s New 3.jpg', N'The Carnival is debuting new and spruced up rides with sensational lighting displays.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1104, 11, 100, N'The Air Race: ', N' World premiere at The Fair! Adults sit in small, airplane-like cars and do barrel rolls around the exciting, LED-covered ride.', N'What''s New 4.jpg', N'The Air Race: World premiere at The Fair! Adults sit in small, airplane-like cars and do barrel rolls around the exciting, LED-covered ride. ', NULL, 1, CAST(0x0000A354017A1D8B AS DateTime), CAST(0x0000A354017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1105, 11, 100, N'Zero Gravity:', N' Another premiere! LED lights change colors as it spins, whirls, and lifts up in the air, giving you the feeling of Zero Gravity!', N'What''s New 5.jpg', N'Zero Gravity: Another premiere! LED lights change colors as it spins, whirls, and lifts up in the air, giving you the feeling of Zero Gravity!', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1106, 11, 100, N'The Circus Train:', N' Ride around the exciting circus! All ages.', N'What''s New 6.jpg', N'The Circus Train: Ride around the exciting circus! All ages.', NULL, 1, CAST(0x0000A354017A1D8B AS DateTime), CAST(0x0000A354017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1107, 11, 100, N'The Crazy Outback: ', N'Go “down under” in the new funhouse featuring Australian cartoon characters!', N'What''s New 7.JPG', N'The Crazy Outback: Go “down under” in the new funhouse featuring Australian cartoon characters!', NULL, 1, CAST(0x0000A354017A1D8B AS DateTime), CAST(0x0000A354017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1108, 11, 100, N'Magic Maze:', N' Flex your mental muscles and navigate your way through a huge glass maze for all ages.', N'What''s New 8.JPG', N'Magic Maze: Flex your mental muscles and navigate your way through a huge glass maze for all ages.', NULL, 1, CAST(0x0000A354017A1D8B AS DateTime), CAST(0x0000A354017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1109, 11, 100, N'The Claw:', N' Zooms up in the air, spin them around, land safely in the glow of the LED lights!', N'What''s New 9.JPG', N'The Claw: Zooms up in the air, spin them around, land safely in the glow of the LED lights!', NULL, 1, CAST(0x0000A354017A1D8B AS DateTime), CAST(0x0000A354017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1201, 12, 100, N'Hours:', N' Friday, August 1 – Saturday, August 9: 10 a.m. – 10 p.m.', N'Info 1.jpg', N'Hours: Friday, August 1 – Saturday, August 9: 10 a.m. – 10 p.m.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1202, 12, 100, N'Accessibility:', N' The Fairgrounds has ample accessible parking, paved roads and paths, and accessible facilities.', N'Info 2.jpg', N'Accessibility: The Fairgrounds has ample accessible parking, paved roads and paths, and accessible facilities.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1203, 12, 100, N'Weather:', N' The Fair continues rain or shine! Exhibits continue regardless of weather; rides will close for heavy rain or lightning.', N'Info 3.jpg', N'Weather: The Fair continues rain or shine! Exhibits continue regardless of weather; rides will close for heavy rain or lightning.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1204, 12, 100, N'What to do:', N' The Fair boasts activities for all ages and interests. Enjoy educational activities, thrilling rides, food vendors, and more!', N'Info 4.jpg', N'What to do: The Fair boasts activities for all ages and interests. Enjoy educational activities, thrilling rides, food vendors, and more!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1205, 12, 100, N'Ticket Prices:', N' $12 adults, $9 seniors $6 children 6-12, free for children under 6, $8 adults after 8:30, vetrans free! Admission and Carnival Megapass: $28 Adults, $24 children. Daily specials, check our events!', N'Info 5.jpg', N'Ticket Prices: $12 adults, $9 seniors $6 children 6-12, free for children under 6, $8 adults after 8:30, vetrans free! Admission and Carnival Megapass: $28 Adults, $24 children. Daily specials, check our events!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1401, 14, 100, N'August 1: 5 p.m. Opening ceremonies in the Horse Show main ring.', N'Country Music Competition: Newton and High Point!', N'Daily Highlights 1.jpg', N'August 1: 5 p.m. Opening ceremonies in the Horse Show main ring.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1402, 14, 100, N'August 2: 7 p.m.', N'Queen of the Fair Pageant in the main ring.', N'Daily Highlights 2.jpg', N'August 2: 7 p.m. Queen of the Fair Pageant in the main ring.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1403, 14, 100, N'August 3:', N'Demolition Derby! Reserved seating $13, general admission $11.', N'Daily Highlights 3.jpg', N'August 3: Demolition Derby! Reserved seating $13, general admission $11.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1404, 14, 100, N'August 4: Green Day! 10 a.m. to 4 p.m.', N'exhibits and presentations in the Performing Arts Tent.', N'Daily Highlights 4.jpg', N'August 4: Green Day! 10 a.m. to 4 p.m. exhibits and presentations in the Performing Arts Tent.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1405, 14, 100, N'August 5:', N'Children’s Day: Gate admission is $5 for children 6-12 years. ', N'Daily Highlights 5.jpg', N'Auguest 5:Children’s Day: Gate admission is $5 for children 6-12 years. ', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1406, 14, 100, N'August 6:', N'Animal of the Day Program in the AG Learning Center. Enjoy educational materials, giveaways, and animal visits. ', N'Daily Highlights 6.jpg', N'Auguest 6:Animal of the Day Program in the AG Learning Center. Enjoy educational materials, giveaways, and animal visits.  ', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1407, 14, 100, N'August 7:', N'Senior Day: 60+$4. Exhibits, music, and giveaways in the morning in the Performing Arts Tent.', N'Daily Highlights 7.jpg', N'August 7: Senior Day: 60+$4. Exhibits, music, and giveaways in the morning in the Performing Arts Tent.', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1408, 14, 100, N'August 8:', N'Animal of the Day Program in the AG Learning Center. Enjoy educational materials, giveaways, and animal visits. ', N'Daily Highlights 8.jpg', N'August 8:Animal of the Day Program in the AG Learning Center. Enjoy educational materials, giveaways, and animal visits. ', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1409, 14, 100, N'August 9:', N'The Sussex County Grand Prix', N'Daily Highlights 9.jpg', N'August 9: The Sussex County Grand Prix', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1410, 14, 100, N'August 10:', N'Last Blast! $8 adults admission, children/seniors free!', N'Daily Highlights 10.jpg', N'Auguest 10:Last Blast! $8 adults admission, children/seniors free!', NULL, 1, CAST(0x0000A33F017A1D8B AS DateTime), CAST(0x0000A33F017A1D8B AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1501, 15, 100, N'Agriculture Area:', N' 4 open-sided livestock barns with livestock rotating throughout the Fair.', N'Fun 1.jpg', N'Agriculture Area: 4 open-sided livestock barns with livestock rotating throughout the Fair.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1502, 15, 100, N'Livestock Pavilion: ', N'Daily livestock shows, milking parlor; greenhouse with vegetable & forage show.', N'Fun 2.jpg', N'Livestock Pavilion: Daily livestock shows, milking parlor; greenhouse with vegetable & forage show.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1503, 15, 100, N'Conservatory & Courtyard:', N'Flower & Garden Expo and Landscape Design competition.', N'Fun 3.jpg', N'Conservatory & Courtyard: Flower & Garden Expo and Landscape Design competition.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1504, 15, 100, N'RoNetco Supermarkets Farm Fun Building:', N'Hands-on activities for children, Melody Farm Follies singing vegetables.', N'Fun 4.jpg', N'RoNetco Supermarkets Farm Fun Building: Hands-on activities for children, Melody Farm Follies singing vegetables.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1505, 15, 100, N'Horse Show Area:', N'Three rings alternate shows including quarter horses and the Sussex County Horse Show! ', N'Fun 5.jpg', N'Horse Show Area: Three rings alternate shows including quarter horses and the Sussex County Horse Show! ', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1506, 15, 100, N'Outdoor Entertainment Area:', N'Demo derbies, lawnmower races, and monster truck rides on varying days. Check the schedule for more details!', N'Fun 6.jpg', N'Outdoor Entertainment Area: Demo derbies, lawnmower races, and monster truck rides on varying days. Check the schedule for more details!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1507, 15, 100, N'RoNetco Supermarkets Family Entertainment Area:', N'Circus Hollywood with petting zoo, racing pigs, pony rides, chainsaw artist, and performances daily.', N'Fun 7.jpg', N'RoNetco Supermarkets Family Entertainment Area: Circus Hollywood with petting zoo, racing pigs, pony rides, chainsaw artist, and performances daily.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1508, 15, 100, N'Carnival:', N'Exciting ride including The Air Race, Zero Gravity, Circus Train, the Crazy Outback, Magic Maze, and The Claw!', N'Fun 8.jpg', N'Carnival: Exciting ride including The Air Race, Zero Gravity, Circus Train, the Crazy Outback, Magic Maze, and The Claw!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1509, 15, 100, N'The Richards Educational Building:', N'Art Show, Photography Show, Creative Arts for Home and Hobby, The Fair History Exhibit, Grange Exhibit & Honey Show. ', N'Fun 9.jpg', N'The Richards Educational Building: Art Show, Photography Show, Creative Arts for Home and Hobby, The Fair History Exhibit, Grange Exhibit & Honey Show.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1510, 15, 100, N'Shotwell 4-H Building: ', N'4-H exhibits & 4-H club booths with daily presentations.  ', N'Fun 10.jpg', N'Shotwell 4-H Building: 4-H exhibits & 4-H club booths with daily presentations.  ', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1511, 15, 100, N'Sussex County Building: ', N'Information about the municipalities and services in Sussex County.', N'Fun 11.jpg', N'Sussex County Building: Information about the municipalities and services in Sussex County.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1601, 16, 100, N'2014 Vendor Application', N'Applications for the 2014 Fair are now', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1602, 16, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1603, 16, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1604, 16, 100, N'Lorem Ipsum Text', N'Lorem ipsum dumy text Lorem', N'star.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1701, 17, 100, N'Lorem Ipsum', N'2014 Vendor Application', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1702, 17, 100, N'Lorem Ipsum', N'2014 Vendor Application', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1703, 17, 100, N'Lorem Ipsum', N'2014 Vendor Application', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1704, 17, 100, N'Lorem Ipsum', N'2014 Vendor Application', N'fun-pic.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1801, 18, 100, N'Facebook', N'', N'facebook.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1802, 18, 100, N'Twiiter', N'', N'twitter.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1803, 18, 100, N'YouTube', N'', N'youtube.png', N'Lorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum TextLorem Ipsum Text', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1901, 19, 100, N'Crawfish Fest: ', N'Enjoy some of the tastiest crawfish in the tri-state area!', N'star.png', N'Crawfish Fest: Enjoy some of the tastiest crawfish in the tri-state area!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1902, 19, 100, N'Champion of the Grill:', N'Get the BBQs ready, because this annual event is smokin’ hot!', N'star.png', N'Champion of the Grill: Get the BBQs ready, because this annual event is smokin’ hot!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1903, 19, 100, N'Richards Building:', N'Hosts the Newton Kennel Club, Peters valley Craft Show, and several fundraising craft shows for local schools and organizations.', N'star.png', N'Richards Building: Hosts the Newton Kennel Club, Peters valley Craft Show, and several fundraising craft shows for local schools and organizations.', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1904, 19, 100, N'Sussex County Farmers’ Market:', N'Recently awarded by the Sussex County Chamber of Commerce, the market brings the bounty of the agricultural community to everyone!', N'star.png', N'Sussex County Farmers’ Market: Recently awarded by the Sussex County Chamber of Commerce, the market brings the bounty of the agricultural community to everyone!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1905, 19, 100, N'Conservatory & Courtyard: ', N'An extraordinary location for business and social events—available for rentals!', N'star.png', N'Conservatory & Courtyard: An extraordinary location for business and social events—available for rentals!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (1906, 19, 100, N'SpringFest Flower & Garden Show:', N'Display flowers and learn more about gardening at this even that attracts a large audience from all over the Tri-State area!', N'star.png', N'SpringFest Flower & Garden Show: Display flowers and learn more about gardening at this even that attracts a large audience from all over the Tri-State area!', NULL, 1, CAST(0x0000A3400061848D AS DateTime), CAST(0x0000A3400061848D AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (2101, 21, 100, N'The Carnival', N'The Carnival', N'Carnival 1.jpg', N'<p>The 2014 New Jersey State Fair is debuting new and spruced up rides with sensational lighting displays! Enjoy thrilling, spinning rides, or relax with some beautiful lights. The Carnival opens at noon every day and runs till closing. Individual ride tickets are available every day.</p><br><p>Pay One Price days: $22 for ride bracelet + required admission<br />Monday, Aug. 4: noon-closing<br />Tuesday-Thursday, Aug. 5-7: noon-6 p.m.<br />Sunday, Aug. 10: 11 a.m.-5 p.m.</p><br><p>MegaPasses: ride bracelet and admission good for any one day—only available before Aug. 1st! $24 child [6-12], $28 adult [13-up]</p><br><p>Our carnival includes classic rides and new thrills!</p><br><p>The Air Race: World premiere at The Fair! Adults sit in small, airplane-like cars and do barrel rolls around the exciting, LED-covered ride. </p><br><p>Zero Gravity: Another premiere! LED lights change colors as it spins, whirls, and lifts up in the air, giving you the feeling of Zero Gravity!</p><br><p>The Circus Train: Ride around the exciting circus! All ages.</p><br><p>The Crazy Outback: Go “down under” in the new funhouse featuring Australian cartoon characters!</p><br><p>Magic Maze: Flex your mental muscles and navigate your way through a huge glass maze for all ages.</p><br><p>The Claw: Zooms up in the air, spin them around, land safely in the glow of the LED lights!</p>', NULL, 1, CAST(0x0000A36601703B81 AS DateTime), CAST(0x0000A36601703B81 AS DateTime), NULL)
INSERT [dbo].[PageItem] ([PageItemId], [PageId], [PageItemType], [PageHeaderText], [PageSubHeaderText], [PageItemImage], [PageItemDetailText], [PageItemSubDetail], [StatusId], [CreatedOn], [ActivatedOn], [UpdatedOn]) VALUES (2201, 22, 100, N'Circus Hollywood', N'Circus Hollywood', N'Circus 1.jpg', N'<p>This year’s Fair debuts a real circus, free of charge with admission! See animals, racing pigs, a petting zoo and several free shows daily under the Big Top! In addition to furry friends, trained acrobats will perform daring aerial displays, and silly clowns will entertain all Fairgoers. Events and animals change daily, so make sure to check it out every day that you attend! </p><br><p>Friday, Aug. 1-Saturday, Aug. 9<br />10 a.m.-9 p.m. Petting zoo &amp; pony rides-sponsored by PNC Bank</p><br><p>Friday, Aug. 1 <br />2 &amp; 7 p.m. Performances<br />12, 3:30, 5:30 p.m. Racing pigs sponsored by EarthCare</p><br><p>Saturday, Aug. 2, Sunday Aug. 3<br />12, 2 &amp; 7 p.m. Performances<br />11 a.m., 3:30, 5:30 p.m. Racing pigs sponsored by EarthCare </p><br><p>Monday, Aug. 3-Friday Aug. 8: <br />2 &amp; 7 p.m. Performances<br />12, 3:30, 5:30 p.m. Racing pigs sponsored by EarthCare</p><br><p>Saturday, Aug. 9<br />2 &amp; 7 p.m. Performances<br />11 a.m., 3:30, 5:30 p.m. Racing pigs sponsored by EarthCare </p><br><p>Sunday, Aug. 10<br />2 p.m. Performance<br />12, 3:30, 5:30 p.m. Racing pigs sponsored by EarthCare</p>', NULL, 1, CAST(0x0000A36601703B81 AS DateTime), CAST(0x0000A36601703B81 AS DateTime), NULL)
/****** Object:  Table [dbo].[PageItemDetail]    Script Date: 07/13/2014 01:36:48 ******/
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
/****** Object:  Table [dbo].[PageHeader]    Script Date: 07/13/2014 01:36:48 ******/
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
/****** Object:  Table [dbo].[Event]    Script Date: 07/13/2014 01:36:48 ******/
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
/****** Object:  ForeignKey [FK_Event_Page]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Page]
GO
/****** Object:  ForeignKey [FK_Event_PageItem]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_PageItem]
GO
/****** Object:  ForeignKey [FK_PageHeader_Page]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageHeader]  WITH CHECK ADD  CONSTRAINT [FK_PageHeader_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageHeader] CHECK CONSTRAINT [FK_PageHeader_Page]
GO
/****** Object:  ForeignKey [FK_PageHeader_PageItem]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageHeader]  WITH CHECK ADD  CONSTRAINT [FK_PageHeader_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[PageHeader] CHECK CONSTRAINT [FK_PageHeader_PageItem]
GO
/****** Object:  ForeignKey [FK_PageItem_Page]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageItem]  WITH CHECK ADD  CONSTRAINT [FK_PageItem_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageItem] CHECK CONSTRAINT [FK_PageItem_Page]
GO
/****** Object:  ForeignKey [FK_PageItemDetail_Page]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageItemDetail]  WITH CHECK ADD  CONSTRAINT [FK_PageItemDetail_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageItemDetail] CHECK CONSTRAINT [FK_PageItemDetail_Page]
GO
/****** Object:  ForeignKey [FK_PageItemDetail_PageItem]    Script Date: 07/13/2014 01:36:48 ******/
ALTER TABLE [dbo].[PageItemDetail]  WITH CHECK ADD  CONSTRAINT [FK_PageItemDetail_PageItem] FOREIGN KEY([PageItemId])
REFERENCES [dbo].[PageItem] ([PageItemId])
GO
ALTER TABLE [dbo].[PageItemDetail] CHECK CONSTRAINT [FK_PageItemDetail_PageItem]
GO
