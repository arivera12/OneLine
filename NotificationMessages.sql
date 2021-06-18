CREATE TABLE [dbo].[NotificationMessages](
	[NotificationMessageId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[IconUri] [nvarchar](512) NULL,
	[DestinationUri] [nvarchar](256) NULL,
	[IsReaded] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_NotificationMessages] PRIMARY KEY CLUSTERED 
(
	[NotificationMessageId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]