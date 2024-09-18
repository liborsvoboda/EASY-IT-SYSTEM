
 IF OBJECT_ID('[dbo].[WebDeveloperNewsList]') IS NOT NULL 
 DROP TABLE [dbo].[WebDeveloperNewsList] 
 GO
 CREATE TABLE [dbo].[WebDeveloperNewsList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Title]        VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_WebDeveloperNewsList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_WebDeveloperNewsList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebDeveloperNewsList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebDeveloperNewsList]  UNIQUE      NONCLUSTERED ([Title] asc) ,
 CONSTRAINT [FK_WebDeveloperNewsList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 