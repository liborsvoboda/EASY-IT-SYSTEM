
 IF OBJECT_ID('[dbo].[GithubSrvTeamRepositoryRoleList]') IS NOT NULL 
 DROP TABLE [dbo].[GithubSrvTeamRepositoryRoleList] 
 GO
 CREATE TABLE [dbo].[GithubSrvTeamRepositoryRoleList] ( 
 [Id]            INT              IDENTITY(1,1)          NOT NULL,
 [TeamId]        INT                                     NOT NULL,
 [RepositoryId]  INT                                     NOT NULL,
 [AllowRead]     BIT                                     NOT NULL,
 [AllowWrite]    BIT                                     NOT NULL,
 [TimeStamp]     DATETIME2                               NOT NULL  CONSTRAINT [DF_GithubSrvTeamRepositoryRoleList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_GithubSrvTeamRepositoryRoleList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_GithubSrvTeamRepositoryRoleList]  UNIQUE      NONCLUSTERED ([TeamId] asc, [RepositoryId] asc) ,
 CONSTRAINT [FK_GithubSrvTeamRepositoryRoleList_RepositoryId] FOREIGN KEY ([RepositoryId]) REFERENCES [dbo].[GithubSrvRepositoryList] (Id)  ON DELETE CASCADE ,
 CONSTRAINT [FK_GithubSrvTeamRepositoryRoleList_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[GithubSrvTeamList] (Id)  ON DELETE CASCADE )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_GithubSrvTeamRepositoryRoleList_RepositoryId] 
    ON [dbo].[GithubSrvTeamRepositoryRoleList] ([RepositoryId] asc)