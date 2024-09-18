
 IF OBJECT_ID('[dbo].[DocSrvDocumentationGroupList]') IS NOT NULL 
 DROP TABLE [dbo].[DocSrvDocumentationGroupList] 
 GO
 CREATE TABLE [dbo].[DocSrvDocumentationGroupList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Sequence]     INT                                     NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_DocumentationGroupList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_DocumentationGroupList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_DocumentationGroupList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_DocumentationGroupList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_DocumentationGroupList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 