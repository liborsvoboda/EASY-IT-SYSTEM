
 IF OBJECT_ID('[dbo].[SolutionStaticFilePathList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionStaticFilePathList] 
 GO
 CREATE TABLE [dbo].[SolutionStaticFilePathList] ( 
 [Id]         INT              IDENTITY(1,1)          NOT NULL,
 [WebsiteId]  INT                                     NOT NULL,
 [Path]       VARCHAR(512)                            NOT NULL,
 [Size]       INT                                     NOT NULL,
 [Active]     BIT                                     NOT NULL  CONSTRAINT [DF_SolutionStaticFilePathList_Active] DEFAULT ((1)),
 [UserId]     INT                                         NULL,
 [TimeStamp]  DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionStaticFilePathList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionStaticFilePathList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SolutionStaticFilePathList]  UNIQUE      NONCLUSTERED ([Path] asc, [UserId] asc) ,
 CONSTRAINT [FK_SolutionStaticFilePathList_SolutionWebsiteList] FOREIGN KEY ([WebsiteId]) REFERENCES [dbo].[SolutionWebsiteList] (Id)  ON DELETE CASCADE ,
 CONSTRAINT [FK_SolutionStaticFilePathList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 