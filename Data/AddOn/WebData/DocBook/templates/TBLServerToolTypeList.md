
 IF OBJECT_ID('[dbo].[ServerToolTypeList]') IS NOT NULL 
 DROP TABLE [dbo].[ServerToolTypeList] 
 GO
 CREATE TABLE [dbo].[ServerToolTypeList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Sequence]     INT                                     NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_ServerToolTypeList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ServerToolTypeList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ServerToolTypeList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_ServerToolTypeList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 