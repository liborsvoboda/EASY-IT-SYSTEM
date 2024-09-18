
 IF OBJECT_ID('[dbo].[BusinessNotesList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessNotesList] 
 GO
 CREATE TABLE [dbo].[BusinessNotesList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_NotesList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_NotesList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_NotesList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_NotesList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_NotesList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 