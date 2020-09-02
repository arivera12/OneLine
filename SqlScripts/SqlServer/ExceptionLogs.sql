CREATE TABLE [dbo].[ExceptionLogs](
	[ExceptionLogId] [nvarchar](128) NOT NULL,
	[HResult] [int] NULL,
	[HelpLink] [nvarchar](4000) NULL,
	[InnerException] [nvarchar](4000) NULL,
	[Message] [nvarchar](4000) NULL,
	[Source] [nvarchar](4000) NULL,
	[StackTrace] [nvarchar](4000) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_exceptionlogs_ExceptionLogID] PRIMARY KEY CLUSTERED 
(
	[ExceptionLogId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]