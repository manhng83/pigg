USE [master]
GO
/****** Object:  Database [Pigg.Data.PiggDbContext]    Script Date: 4/26/2021 10:46:53 PM ******/
CREATE DATABASE [Pigg.Data.PiggDbContext] ON  PRIMARY 
( NAME = N'Pigg.Data.PiggDbContext', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Pigg.Data.PiggDbContext.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Pigg.Data.PiggDbContext_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Pigg.Data.PiggDbContext_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Pigg.Data.PiggDbContext].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET ARITHABORT OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET  MULTI_USER 
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET DB_CHAINING OFF 
GO
USE [Pigg.Data.PiggDbContext]
GO
/****** Object:  Table [dbo].[ContentList]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentList](
	[ContentListId] [int] IDENTITY(1,1) NOT NULL,
	[WebPartPlacementId] [int] NULL,
	[Visible] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ContentList] PRIMARY KEY CLUSTERED 
(
	[ContentListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContentListItem]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentListItem](
	[ContentListItemId] [int] IDENTITY(1,1) NOT NULL,
	[ContentListId] [int] NOT NULL,
	[LanguageIsoCode] [nvarchar](15) NOT NULL,
	[ParentContentListItemId] [int] NULL,
	[Title] [nvarchar](255) NOT NULL,
	[LongTitle] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[PageContent] [nvarchar](max) NULL,
	[Keywords] [nvarchar](max) NULL,
	[IsPublished] [bit] NOT NULL,
	[IsFrontPage] [bit] NOT NULL,
	[ShowInList] [bit] NOT NULL,
	[OrderInList] [decimal](18, 2) NULL,
 CONSTRAINT [PK_dbo.ContentListItem] PRIMARY KEY CLUSTERED 
(
	[ContentListItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomPage]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomPage](
	[PageId] [int] IDENTITY(1,1) NOT NULL,
	[ParentCustomPageId] [int] NULL,
	[CultureCode] [nvarchar](15) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[LongTitle] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[PageContent] [nvarchar](max) NULL,
	[Keywords] [nvarchar](max) NULL,
	[IsPublished] [bit] NOT NULL,
	[IsFrontPage] [bit] NOT NULL,
	[ShowInList] [bit] NOT NULL,
	[OrderInList] [decimal](18, 2) NULL,
	[EntityId] [uniqueidentifier] NOT NULL,
	[CustomPage2_CustomPageId] [int] NULL,
 CONSTRAINT [PK_dbo.CustomPage] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomSetting]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomSetting](
	[CustomSettingId] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](150) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Reserved] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.CustomSetting] PRIMARY KEY CLUSTERED 
(
	[CustomSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebPartPlacement]    Script Date: 4/26/2021 10:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebPartPlacement](
	[WebPartPlacementId] [int] IDENTITY(1,1) NOT NULL,
	[WebPartZone] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.WebPartPlacement] PRIMARY KEY CLUSTERED 
(
	[WebPartPlacementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[People] ON 

INSERT [dbo].[People] ([Id], [Email]) VALUES (1, N'admin')
SET IDENTITY_INSERT [dbo].[People] OFF
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (1, CAST(N'2021-04-26T15:38:40.193' AS DateTime), NULL, 1, NULL, 0, N'AIyIXSSGPo1lrrW/rM7BxUyA7Yznvd7/5tt/Au65uqRQk7UsFxQayAJEtmn7CYONRQ==', CAST(N'2021-04-26T15:38:40.193' AS DateTime), N'', NULL, NULL)
GO
/****** Object:  Index [IX_WebPartPlacementId]    Script Date: 4/26/2021 10:46:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_WebPartPlacementId] ON [dbo].[ContentList]
(
	[WebPartPlacementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ContentListId]    Script Date: 4/26/2021 10:46:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_ContentListId] ON [dbo].[ContentListItem]
(
	[ContentListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomPage2_CustomPageId]    Script Date: 4/26/2021 10:46:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomPage2_CustomPageId] ON [dbo].[CustomPage]
(
	[CustomPage2_CustomPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ParentCustomPageId]    Script Date: 4/26/2021 10:46:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_ParentCustomPageId] ON [dbo].[CustomPage]
(
	[ParentCustomPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__webpages__8A2B6160AF408E4B]    Script Date: 4/26/2021 10:46:53 PM ******/
ALTER TABLE [dbo].[webpages_Roles] ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO
ALTER TABLE [dbo].[ContentList]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ContentList_dbo.WebPartPlacement_WebPartPlacementId] FOREIGN KEY([WebPartPlacementId])
REFERENCES [dbo].[WebPartPlacement] ([WebPartPlacementId])
GO
ALTER TABLE [dbo].[ContentList] CHECK CONSTRAINT [FK_dbo.ContentList_dbo.WebPartPlacement_WebPartPlacementId]
GO
ALTER TABLE [dbo].[ContentListItem]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ContentListItem_dbo.ContentList_ContentListId] FOREIGN KEY([ContentListId])
REFERENCES [dbo].[ContentList] ([ContentListId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContentListItem] CHECK CONSTRAINT [FK_dbo.ContentListItem_dbo.ContentList_ContentListId]
GO
ALTER TABLE [dbo].[CustomPage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomPage_dbo.CustomPage_CustomPage2_CustomPageId] FOREIGN KEY([CustomPage2_CustomPageId])
REFERENCES [dbo].[CustomPage] ([PageId])
GO
ALTER TABLE [dbo].[CustomPage] CHECK CONSTRAINT [FK_dbo.CustomPage_dbo.CustomPage_CustomPage2_CustomPageId]
GO
ALTER TABLE [dbo].[CustomPage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomPage_dbo.CustomPage_ParentCustomPageId] FOREIGN KEY([ParentCustomPageId])
REFERENCES [dbo].[CustomPage] ([PageId])
GO
ALTER TABLE [dbo].[CustomPage] CHECK CONSTRAINT [FK_dbo.CustomPage_dbo.CustomPage_ParentCustomPageId]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[People] ([Id])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_UserId]
GO
USE [master]
GO
ALTER DATABASE [Pigg.Data.PiggDbContext] SET  READ_WRITE 
GO
