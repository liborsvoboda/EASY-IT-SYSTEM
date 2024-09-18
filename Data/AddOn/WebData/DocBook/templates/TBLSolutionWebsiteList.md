
 IF OBJECT_ID('[dbo].[SolutionWebsiteList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionWebsiteList] 
 GO
 CREATE TABLE [dbo].[SolutionWebsiteList] ( 
 [Id]                       INT              IDENTITY(1,1)          NOT NULL,
 [WebsiteName]              VARCHAR(50)                             NOT NULL,
 [Description]              TEXT                                        NULL,
 [MinimalReadAccessValue]   INT                                     NOT NULL  CONSTRAINT [DF_SolutionWebsiteList_MinimalReadAccessValue] DEFAULT ((0)),
 [MinimalWriteAccessValue]  INT                                     NOT NULL  CONSTRAINT [DF_SolutionWebsiteList_MinimalWriteAccessValue] DEFAULT ((0)),
 [UserId]                   INT                                     NOT NULL,
 [Active]                   BIT                                     NOT NULL  CONSTRAINT [DF_SolutionWebsiteList_Active] DEFAULT ((1)),
 [TimeStamp]                DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionWebsiteList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionWebsiteList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SolutionWebsiteList]  UNIQUE      NONCLUSTERED ([WebsiteName] asc) ,
 CONSTRAINT [FK_SolutionWebsiteList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 