
 IF OBJECT_ID('[dbo].[SolutionLanguageList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionLanguageList] 
 GO
 CREATE TABLE [dbo].[SolutionLanguageList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [SystemName]   VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_ServerLanguageList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ServerLanguageList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ServerLanguageList]  UNIQUE      NONCLUSTERED ([SystemName] asc) ,
 CONSTRAINT [FK_ServerLanguageList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 