CREATE TABLE [dbo].[UserBlobs](
	[UserBlobId] [nvarchar](128) NOT NULL,
	[UserIdentifier] [nvarchar](450) NOT NULL,
	[ContentDisposition] [nvarchar](128) NULL,
	[ContentType] [nvarchar](128) NULL,
	[FileName] [nvarchar](512) NOT NULL,
	[Name] [nvarchar](512) NULL,
	[Length] [bigint] NOT NULL,
	[FilePath] [nvarchar](512) NOT NULL,
	[TableName] [nvarchar](128) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_UserBlob] PRIMARY KEY CLUSTERED 
(
	[UserBlobId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]