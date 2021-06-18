ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]

ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]

ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]

ALTER TABLE [dbo].[UserBlobs]  WITH CHECK ADD  CONSTRAINT [FK_UserBlobs_AspNetUsers] FOREIGN KEY([UserIdentifier])
REFERENCES [dbo].[AspNetUsers] ([Id])

SET IDENTITY_INSERT [dbo].[TeleCommunicationServiceProviders] ON 
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (1, N'AT&T', N'@mms.att.net', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (2, N'Claro', N'@vtexto.com', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (3, N'Comcast', N'@comcastpcs.textmsg.com', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (4, N'Liberty', N'@mms.att.net', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (5, N'Sprint', N'@pm.sprint.com', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (6, N'T-Mobile', N'@tmomail.net', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[TeleCommunicationServiceProviders] ([TeleCommunicationServiceProviderId], [Name], [DomainGateway], [IsDeleted], [RecordRevisions], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (7, N'Verizon Wireless', N'@vzwpix.com', 0, 0, N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime), N'0', CAST(N'2021-04-08T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[TeleCommunicationServiceProviders] OFF