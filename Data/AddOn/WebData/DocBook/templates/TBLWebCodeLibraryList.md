
 IF OBJECT_ID('[dbo].[WebCodeLibraryList]') IS NOT NULL 
 DROP TABLE [dbo].[WebCodeLibraryList] 
 GO
 CREATE TABLE [dbo].[WebCodeLibraryList] ( 
 [Id]                 INT              IDENTITY(1,1)          NOT NULL,
 [InheritedCodeType]  VARCHAR(50)                                 NULL,
 [Name]               VARCHAR(50)                             NOT NULL,
 [Description]        VARCHAR(2096)                               NULL,
 [Content]            VARCHAR(max)                            NOT NULL,
 [IsCompletion]       BIT                                     NOT NULL  CONSTRAINT [DF_WebCodeLibraryList_IsCompletion] DEFAULT ((0)),
 [UserId]             INT                                     NOT NULL,
 [TimeStamp]          DATETIME2                               NOT NULL  CONSTRAINT [DF_WebCodeLibraryList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebCodeLibraryList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebCodeLibraryList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_WebCodeLibraryList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 