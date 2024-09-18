
 IF OBJECT_ID('[dbo].[GithubSrvSshKeyList]') IS NOT NULL 
 DROP TABLE [dbo].[GithubSrvSshKeyList] 
 GO
 CREATE TABLE [dbo].[GithubSrvSshKeyList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [UserId]       INT                                     NOT NULL,
 [KeyType]      NVARCHAR(max)                               NULL,
 [Fingerprint]  NVARCHAR(max)                               NULL,
 [PublicKey]    NVARCHAR(max)                               NULL,
 [ImportData]   DATETIME2                               NOT NULL,
 [LastUse]      DATETIME2                               NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_GithubSrvSshKeyList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_GithubSrvSshKeyList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_GithubSrvSshKeyList_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id)  ON DELETE CASCADE )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_GithubSrvSshKeyList_UserId] 
    ON [dbo].[GithubSrvSshKeyList] ([UserId] asc)