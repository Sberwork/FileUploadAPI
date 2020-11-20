USE [AllSampleCode]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 23-01-2020 9.58.54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[FileType] [varchar](100) NULL,
	[DataFiles] [varbinary](max) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
