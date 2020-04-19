CREATE TABLE [dbo].[UserBlobs](
	[UserBlobId] [nvarchar](128) NOT NULL,
	[ContentDisposition] [nvarchar](100) NULL,
    [ContentType] [nvarchar](100) NULL,
    [FileName] [nvarchar](500) NOT NULL,
    [Name] [nvarchar](100) NULL,
    [Length] [BIGINT] NOT NULL,
    [FilePath] [nvarchar](500) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](128) NULL,
    CONSTRAINT [PK_UserBlob] PRIMARY KEY CLUSTERED 
(
	[UserBlobId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]