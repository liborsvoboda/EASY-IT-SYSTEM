
 IF OBJECT_ID('[dbo].[WebDocumentationCodeLibraryList]') IS NOT NULL 
 DROP TABLE [dbo].[WebDocumentationCodeLibraryList] 
 GO
 CREATE TABLE [dbo].[WebDocumentationCodeLibraryList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Description]  VARCHAR(2096)                               NULL,
 [MdContent]    TEXT                                    NOT NULL,
 [HtmlContent]  TEXT                                    NOT NULL,
 [UserId]       INT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_WebDocumentationCodeLibraryList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebDocumentationCodeLibraryList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebDocumentationCodeLibraryList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_WebDocumentationCodeLibraryList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 