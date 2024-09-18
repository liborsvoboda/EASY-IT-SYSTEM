
 IF OBJECT_ID('[dbo].[GithubSrvRepositoryList]') IS NOT NULL 
 DROP TABLE [dbo].[GithubSrvRepositoryList] 
 GO
 CREATE TABLE [dbo].[GithubSrvRepositoryList] ( 
 [Id]             INT              IDENTITY(1,1)          NOT NULL,
 [UserId]         INT                                     NOT NULL,
 [UserName]       VARCHAR(150)                                NULL,
 [Name]           VARCHAR(150)                                NULL,
 [Description]    TEXT                                        NULL,
 [DefaultBranch]  VARCHAR(1024)                               NULL,
 [NumIssues]      INT                                     NOT NULL,
 [NumOpenIssues]  INT                                     NOT NULL,
 [NumPulls]       INT                                     NOT NULL,
 [NumOpenPulls]   INT                                     NOT NULL,
 [CreationDate]   DATETIME2                               NOT NULL,
 [IsPrivate]      BIT                                     NOT NULL,
 [IsMirror]       BIT                                     NOT NULL,
 [Size]           BIGINT                                  NOT NULL,
 [UpdateTime]     DATETIME2                               NOT NULL,
 [TimeStamp]      DATETIME2                               NOT NULL  CONSTRAINT [DF_GithubSrvRepositoryList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_GithubSrvRepositoryList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_GithubSrvRepositoryList_SolutionUserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 