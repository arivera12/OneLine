CREATE TABLE [dbo].[SupportedCultures](
	[SupportedCultureId] [nvarchar](128) NOT NULL,
	[Locale] [nvarchar](32) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](128) NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_SupportedCultures] PRIMARY KEY CLUSTERED 
(
	[SupportedCultureId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]