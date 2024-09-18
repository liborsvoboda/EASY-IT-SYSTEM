
 IF OBJECT_ID('[dbo].[DocSrvDocumentationCodeLibraryList]') IS NOT NULL 
 DROP TABLE [dbo].[DocSrvDocumentationCodeLibraryList] 
 GO
 CREATE TABLE [dbo].[DocSrvDocumentationCodeLibraryList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Description]  VARCHAR(2096)                               NULL,
 [MdContent]    TEXT                                    NOT NULL,
 [HtmlContent]  TEXT                                    NOT NULL,
 [UserId]       INT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_DocumentationCodeLibraryList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_DocumentationCodeLibraryList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_DocumentationCodeLibraryList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_DocumentationCodeLibraryList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 