CREATE TABLE [dbo].[AuditTrails](
	[AuditTrailId] [nvarchar](128) NOT NULL,
	[Action] [nvarchar](128) NULL,
	[ActionName] [nvarchar](256) NULL,
	[ControllerName] [nvarchar](128) NULL,
	[TableName] [nvarchar](128) NULL,
	[Record] [nvarchar](max) NULL,
	[Hostname] [nvarchar](128) NULL,
	[RemoteIpAddress] [nvarchar](64) NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_audittrail_AuditTrailID] PRIMARY KEY CLUSTERED 
(
	[AuditTrailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]