CREATE TABLE [dbo].[TeleCommunicationServiceProviders](
	[TeleCommunicationServiceProviderId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[DomainGateway] [nvarchar](64) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[RecordRevisions] [int] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedBy] [nvarchar](128) NULL,
	[LastModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_TeleCommunicationServiceProviders] PRIMARY KEY CLUSTERED 
(
	[TeleCommunicationServiceProviderId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]