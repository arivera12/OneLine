CREATE TABLE [dbo].[AuditTrails](
	[AuditTrailId] [nvarchar](128) NOT NULL,
	[Action] [nvarchar](128) NULL,
	[ActionName] [nvarchar](250) NULL,
	[ControllerName] [nvarchar](100) NULL,
	[TableName] [nvarchar](100) NULL,
	[Record] [nvarchar](max) NULL,
	[Hostname] [nvarchar](100) NULL,
	[RemoteIpAddress] [nvarchar](45) NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[CreatedOn] [datetime] NOT NULL,
	CONSTRAINT [PK_audittrail_AuditTrailID] PRIMARY KEY CLUSTERED 
(
	[AuditTrailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]