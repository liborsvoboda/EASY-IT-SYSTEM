
 IF OBJECT_ID('[dbo].[GithubSrvTeamList]') IS NOT NULL 
 DROP TABLE [dbo].[GithubSrvTeamList] 
 GO
 CREATE TABLE [dbo].[GithubSrvTeamList] ( 
 [Id]            INT              IDENTITY(1,1)          NOT NULL,
 [Name]          VARCHAR(150)                                NULL,
 [Description]   TEXT                                        NULL,
 [CreationDate]  DATETIME2                               NOT NULL,
 [TimeStamp]     DATETIME2                               NOT NULL  CONSTRAINT [DF_GithubSrvTeamList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_GithubSrvTeamList]  PRIMARY KEY CLUSTERED    ([Id] asc) )
 
 