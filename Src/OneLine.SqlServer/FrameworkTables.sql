﻿/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](4000) NULL,
	[ClaimValue] [nvarchar](4000) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](4000) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](4000) NULL,
	[ClaimValue] [nvarchar](4000) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](4000) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](4000) NULL,
	[SecurityStamp] [nvarchar](4000) NULL,
	[ConcurrencyStamp] [nvarchar](4000) NULL,
	[PhoneNumber] [nvarchar](4000) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[IsOnline] [bit] NULL,
	[LastTimeOnline] [datetime] NULL,
	[PreferredCultureLocale] [nvarchar](32) NULL,
	[IsMobilePhone] [bit] NULL,
	[TeleCommunicationServiceProviderId] [int] NULL,
	[RecordRevisions] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](128) NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](4000) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditTrails]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditTrails](
	[AuditTrailId] [nvarchar](128) NOT NULL,
	[Action] [nvarchar](512) NULL,
	[ActionName] [nvarchar](256) NULL,
	[ControllerName] [nvarchar](128) NULL,
	[TableName] [nvarchar](128) NULL,
	[Record] [nvarchar](max) NULL,
	[Hostname] [nvarchar](128) NULL,
	[RemoteIpAddress] [nvarchar](64) NULL,
	[CreatedBy] [nvarchar](450) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_audittrail_AuditTrailID] PRIMARY KEY CLUSTERED 
(
	[AuditTrailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExceptionLogs]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExceptionLogs](
	[ExceptionLogId] [nvarchar](128) NOT NULL,
	[HResult] [int] NULL,
	[HelpLink] [nvarchar](4000) NULL,
	[InnerException] [nvarchar](max) NULL,
	[Message] [nvarchar](4000) NULL,
	[Source] [nvarchar](4000) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](450) NULL,
 CONSTRAINT [PK_exceptionlogs_ExceptionLogID] PRIMARY KEY CLUSTERED 
(
	[ExceptionLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FirebaseCloudMessagingDeviceRegistrations]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FirebaseCloudMessagingDeviceRegistrations](
	[FirebaseCloudMessagingDeviceRegistrationId] [nvarchar](256) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[IsDeleted] [bit] NULL,
	[RecordRevisions] [int] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedBy] [nvarchar](128) NULL,
	[LastModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_FirebaseCloudMessagingDeviceRegistrations] PRIMARY KEY CLUSTERED 
(
	[FirebaseCloudMessagingDeviceRegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FirebaseCloudMessagingDeviceTopicSubscriptions]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FirebaseCloudMessagingDeviceTopicSubscriptions](
	[FirebaseCloudMessagingDeviceTopicSubscriptionId] [nvarchar](128) NOT NULL,
	[FirebaseCloudMessagingDeviceRegistrationId] [nvarchar](256) NOT NULL,
	[FirebaseCloudMessagingTopicId] [nvarchar](128) NOT NULL,
	[IsDeleted] [bit] NULL,
	[RecordRevisions] [int] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedBy] [nvarchar](128) NULL,
	[LastModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_FirebaseCloudMessagingDeviceTopicSubscriptions] PRIMARY KEY CLUSTERED 
(
	[FirebaseCloudMessagingDeviceTopicSubscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FirebaseCloudMessagingTopics]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FirebaseCloudMessagingTopics](
	[FirebaseCloudMessagingTopicId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[Name] [nvarchar](64) NOT NULL,
	[IsDeleted] [bit] NULL,
	[RecordRevisions] [int] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedBy] [nvarchar](128) NULL,
	[LastModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_FirebaseCloudMessagingTopics] PRIMARY KEY CLUSTERED 
(
	[FirebaseCloudMessagingTopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationMessages]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationMessages](
	[NotificationMessageId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[IconUri] [nvarchar](512) NULL,
	[IsReaded] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_NotificationMessages] PRIMARY KEY CLUSTERED 
(
	[NotificationMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupportedCultures]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportedCultures](
	[SupportedCultureId] [nvarchar](128) NOT NULL,
	[Locale] [nvarchar](32) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](128) NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_SupportedCultures] PRIMARY KEY CLUSTERED 
(
	[SupportedCultureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeleCommunicationServiceProviders]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeleCommunicationServiceProviders](
	[TeleCommunicationServiceProviderId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[DomainGateway] [nvarchar](64) NOT NULL,
	[IsDeleted] [bit] NULL,
	[RecordRevisions] [int] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedBy] [nvarchar](128) NULL,
	[LastModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_TeleCommunicationServiceProviders] PRIMARY KEY CLUSTERED 
(
	[TeleCommunicationServiceProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserBlobs]    Script Date: 8/29/2023 10:35:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBlobs](
	[UserBlobId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[ContentDisposition] [nvarchar](128) NULL,
	[ContentType] [nvarchar](128) NULL,
	[FileName] [nvarchar](512) NOT NULL,
	[Name] [nvarchar](512) NULL,
	[Length] [bigint] NOT NULL,
	[FilePath] [nvarchar](512) NOT NULL,
	[TableName] [nvarchar](128) NOT NULL,
	[IsDeleted] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_UserBlob] PRIMARY KEY CLUSTERED 
(
	[UserBlobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_TeleCommunicationServiceProviders] FOREIGN KEY([TeleCommunicationServiceProviderId])
REFERENCES [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_TeleCommunicationServiceProviders]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AuditTrails]  WITH CHECK ADD  CONSTRAINT [FK_AuditTrails_AspNetUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AuditTrails] CHECK CONSTRAINT [FK_AuditTrails_AspNetUsers]
GO
ALTER TABLE [dbo].[ExceptionLogs]  WITH CHECK ADD  CONSTRAINT [FK_ExceptionLogs_AspNetUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ExceptionLogs] CHECK CONSTRAINT [FK_ExceptionLogs_AspNetUsers]
GO
ALTER TABLE [dbo].[FirebaseCloudMessagingDeviceRegistrations]  WITH CHECK ADD  CONSTRAINT [FK_FirebaseCloudMessagingDeviceRegistrations_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[FirebaseCloudMessagingDeviceRegistrations] CHECK CONSTRAINT [FK_FirebaseCloudMessagingDeviceRegistrations_AspNetUsers]
GO
ALTER TABLE [dbo].[FirebaseCloudMessagingDeviceTopicSubscriptions]  WITH CHECK ADD  CONSTRAINT [FK_FirebaseCloudMessagingDeviceTopicSubscriptions_FirebaseCloudMessagingDeviceRegistrations] FOREIGN KEY([FirebaseCloudMessagingDeviceRegistrationId])
REFERENCES [dbo].[FirebaseCloudMessagingDeviceRegistrations] ([FirebaseCloudMessagingDeviceRegistrationId])
GO
ALTER TABLE [dbo].[FirebaseCloudMessagingDeviceTopicSubscriptions] CHECK CONSTRAINT [FK_FirebaseCloudMessagingDeviceTopicSubscriptions_FirebaseCloudMessagingDeviceRegistrations]
GO
ALTER TABLE [dbo].[FirebaseCloudMessagingDeviceTopicSubscriptions]  WITH CHECK ADD  CONSTRAINT [FK_FirebaseCloudMessagingDeviceTopicSubscriptions_FirebaseCloudMessagingTopics] FOREIGN KEY([FirebaseCloudMessagingTopicId])
REFERENCES [dbo].[FirebaseCloudMessagingTopics] ([FirebaseCloudMessagingTopicId])
GO
ALTER TABLE [dbo].[FirebaseCloudMessagingDeviceTopicSubscriptions] CHECK CONSTRAINT [FK_FirebaseCloudMessagingDeviceTopicSubscriptions_FirebaseCloudMessagingTopics]
GO
ALTER TABLE [dbo].[FirebaseCloudMessagingTopics]  WITH CHECK ADD  CONSTRAINT [FK_FirebaseCloudMessagingTopics_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[FirebaseCloudMessagingTopics] CHECK CONSTRAINT [FK_FirebaseCloudMessagingTopics_AspNetUsers]
GO
ALTER TABLE [dbo].[NotificationMessages]  WITH CHECK ADD  CONSTRAINT [FK_NotificationMessages_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[NotificationMessages] CHECK CONSTRAINT [FK_NotificationMessages_AspNetUsers]
GO
ALTER TABLE [dbo].[UserBlobs]  WITH CHECK ADD  CONSTRAINT [FK_UserBlobs_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserBlobs] CHECK CONSTRAINT [FK_UserBlobs_AspNetUsers]
GO
