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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]