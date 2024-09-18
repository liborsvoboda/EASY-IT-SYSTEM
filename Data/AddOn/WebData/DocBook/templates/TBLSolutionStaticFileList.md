
 IF OBJECT_ID('[dbo].[SolutionStaticFileList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionStaticFileList] 
 GO
 CREATE TABLE [dbo].[SolutionStaticFileList] ( 
 [Id]            INT              IDENTITY(1,1)          NOT NULL,
 [WebsiteId]     INT                                     NOT NULL,
 [StaticPathId]  INT                                     NOT NULL,
 [FileNamePath]  VARCHAR(512)                            NOT NULL,
 [MimeType]      VARCHAR(150)                            NOT NULL,
 [Content]       VARBINARY(max)                              NULL,
 [Active]        BIT                                     NOT NULL  CONSTRAINT [DF_SolutionStaticFileList_Active] DEFAULT ((1)),
 [UserId]        INT                                         NULL,
 [TimeStamp]     DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionStaticFileList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionStaticFileList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SolutionStaticFileList]  UNIQUE      NONCLUSTERED ([FileNamePath] asc, [UserId] asc) ,
 CONSTRAINT [FK_SolutionStaticFileList_SolutionStaticFilePathList] FOREIGN KEY ([StaticPathId]) REFERENCES [dbo].[SolutionStaticFilePathList] (Id)  ON DELETE CASCADE ,
 CONSTRAINT [FK_SolutionStaticFileList_SolutionWebsiteList] FOREIGN KEY ([WebsiteId]) REFERENCES [dbo].[SolutionWebsiteList] (Id) ,
 CONSTRAINT [FK_SolutionStaticFileList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 