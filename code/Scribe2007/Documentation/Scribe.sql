/****** Object:  ForeignKey [FK_Files_Notes]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Files_Notes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Files]'))
ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_Files_Notes]
GO
/****** Object:  ForeignKey [FK_Metadata_Notes]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Metadata_Notes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Metadata]'))
ALTER TABLE [dbo].[Metadata] DROP CONSTRAINT [FK_Metadata_Notes]
GO
/****** Object:  ForeignKey [FK_Tags_Notes]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tags_Notes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tags]'))
ALTER TABLE [dbo].[Tags] DROP CONSTRAINT [FK_Tags_Notes]
GO
/****** Object:  StoredProcedure [dbo].[InsertTag]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertTag]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertTag]
GO
/****** Object:  StoredProcedure [dbo].[InsertNote]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertNote]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertNote]
GO
/****** Object:  StoredProcedure [dbo].[InsertMetadata]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertMetadata]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertMetadata]
GO
/****** Object:  StoredProcedure [dbo].[GetAllNoteFilenames]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllNoteFilenames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllNoteFilenames]
GO
/****** Object:  StoredProcedure [dbo].[GetFile]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetFile]
GO
/****** Object:  StoredProcedure [dbo].[GetAllNotes]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllNotes]
GO
/****** Object:  StoredProcedure [dbo].[InsertFile]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertFile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertFile]
GO
/****** Object:  Table [dbo].[Metadata]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Metadata]') AND type in (N'U'))
DROP TABLE [dbo].[Metadata]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Files]') AND type in (N'U'))
DROP TABLE [dbo].[Files]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
DROP TABLE [dbo].[Tags]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 10/19/2007 20:27:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notes]') AND type in (N'U'))
DROP TABLE [dbo].[Notes]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Notes](
	[NoteId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Text] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tags](
	[TagId] [bigint] IDENTITY(1,1) NOT NULL,
	[NoteId] [bigint] NOT NULL,
	[Tag] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Files]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Files]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Files](
	[FileId] [bigint] IDENTITY(1,1) NOT NULL,
	[NoteId] [bigint] NOT NULL,
	[Filename] [varbinary](max) NOT NULL,
	[Filedata] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Metadata]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Metadata]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Metadata](
	[MetadataId] [bigint] IDENTITY(1,1) NOT NULL,
	[NoteId] [bigint] NOT NULL,
	[Name] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Metadata] PRIMARY KEY CLUSTERED 
(
	[MetadataId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertFile]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertFile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertFile]
    @NoteId BIGINT,
    @Filename VARBINARY(MAX),
    @Filedata VARBINARY(MAX)
AS
BEGIN
    INSERT INTO Files
    (NoteId, Filename, Filedata)
    VALUES
    (@NoteId, @Filename, @Filedata)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllNotes]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllNotes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetAllNotes]
AS
BEGIN
	SELECT [NoteId], [DateCreated], [Text]
	FROM [Notes]
	SELECT [TagId], [NoteId], [Tag]
	FROM [Tags]
	SELECT [FileId], [NoteId], [Filename]
	FROM [Files]
	SELECT [MetadataId], [NoteId], [Name], [Value]
	FROM [Metadata]
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetFile]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFile]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetFile]
	@FileId BIGINT
AS
BEGIN
	SELECT Filedata
	FROM [Files]
	WHERE [FileId] = @FileId
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllNoteFilenames]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllNoteFilenames]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetAllNoteFilenames]
	@NoteId BIGINT
AS
BEGIN
	SELECT m.Name, m.Value, f.FileId, f.Filename
	FROM Metadata m
	INNER JOIN Files f ON m.[NoteId] = f.[NoteId]
	WHERE f.[NoteId] = @NoteId
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertMetadata]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertMetadata]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertMetadata]
    @NoteId BIGINT,
    @Name NVARCHAR(MAX),
    @Value NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Metadata
    (NoteId, Name, Value)
    VALUES
    (@NoteId, @Name, @Value)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertNote]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertNote]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertNote]
    @Text VARBINARY(MAX)
AS
BEGIN
    INSERT INTO Notes
    (DateCreated, Text)
    VALUES
    (GETDATE(), @Text)

    SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertTag]    Script Date: 10/19/2007 20:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertTag]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertTag]
    @NoteId BIGINT,
    @Tag VARBINARY(MAX)
AS
BEGIN
    INSERT INTO Tags
    (NoteId, Tag)
    VALUES
    (@NoteId, @Tag)
END
' 
END
GO
/****** Object:  ForeignKey [FK_Files_Notes]    Script Date: 10/19/2007 20:27:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Files_Notes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Files]'))
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_Notes] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Notes] ([NoteId])
GO
ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_Notes]
GO
/****** Object:  ForeignKey [FK_Metadata_Notes]    Script Date: 10/19/2007 20:27:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Metadata_Notes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Metadata]'))
ALTER TABLE [dbo].[Metadata]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_Notes] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Notes] ([NoteId])
GO
ALTER TABLE [dbo].[Metadata] CHECK CONSTRAINT [FK_Metadata_Notes]
GO
/****** Object:  ForeignKey [FK_Tags_Notes]    Script Date: 10/19/2007 20:27:47 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tags_Notes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tags]'))
ALTER TABLE [dbo].[Tags]  WITH CHECK ADD  CONSTRAINT [FK_Tags_Notes] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Notes] ([NoteId])
GO
ALTER TABLE [dbo].[Tags] CHECK CONSTRAINT [FK_Tags_Notes]
GO
