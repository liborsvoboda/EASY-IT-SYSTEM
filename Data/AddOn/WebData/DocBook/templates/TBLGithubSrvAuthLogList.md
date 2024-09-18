
 IF OBJECT_ID('[dbo].[GithubSrvAuthLogList]') IS NOT NULL 
 DROP TABLE [dbo].[GithubSrvAuthLogList] 
 GO
 CREATE TABLE [dbo].[GithubSrvAuthLogList] ( 
 [Id]         INT              IDENTITY(1,1)          NOT NULL,
 [IssueDate]  DATETIME2                               NOT NULL,
 [Expires]    DATETIME2                               NOT NULL,
 [IssueIp]    NVARCHAR(max)                               NULL,
 [LastIp]     NVARCHAR(max)                               NULL,
 [IsValid]    BIT                                     NOT NULL,
 [UserId]     INT                                     NOT NULL,
 [TimeStamp]  DATETIME2                               NOT NULL  CONSTRAINT [DF_GithubSrvAuthLogList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_GithubSrvAuthLogList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_GithubSrvAuthLogList_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id)  ON DELETE CASCADE )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_GithubSrvAuthLogList_UserId] 
    ON [dbo].[GithubSrvAuthLogList] ([UserId] asc)