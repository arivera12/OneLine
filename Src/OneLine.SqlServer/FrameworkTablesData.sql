SET IDENTITY_INSERT [dbo].[TeleCommunicationServiceProviders] ON
GO
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (1, N'AT&T', N'@mms.att.net', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (2, N'Claro', N'@vtexto.com', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (3, N'Comcast', N'@comcastpcs.textmsg.com', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (4, N'Liberty', N'@mms.att.net', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (5, N'Sprint', N'@pm.sprint.com', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (6, N'T-Mobile', N'@tmomail.net', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (7, N'Verizon Wireless', N'@vzwpix.com', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TeleCommunicationServiceProviders] OFF
GO
INSERT [dbo].[SupportedCultures] ([SupportedCultureId], [Locale], [Name], [IsDeleted], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'en-US', N'en-US', N'English (United States)', 0, N'64666f90-f15e-491f-98df-069d02aa19f9', CAST(N'2021-04-07T23:17:01.770' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[SupportedCultures] ([SupportedCultureId], [Locale], [Name], [IsDeleted], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'es-PR', N'es-PR', N'Spanish (Puerto Rico)', 0, N'64666f90-f15e-491f-98df-069d02aa19f9', CAST(N'2021-04-07T23:17:22.930' AS DateTime), NULL, NULL)
GO
