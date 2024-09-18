
 IF OBJECT_ID('[dbo].[ServerStaticOrMvcDefPathList]') IS NOT NULL 
 DROP TABLE [dbo].[ServerStaticOrMvcDefPathList] 
 GO
 CREATE TABLE [dbo].[ServerStaticOrMvcDefPathList] ( 
 [Id]                    INT              IDENTITY(1,1)          NOT NULL,
 [SystemName]            VARCHAR(50)                             NOT NULL,
 [WebRootSubPath]        VARCHAR(2048)                           NOT NULL,
 [AliasPath]             VARCHAR(255)                                NULL,
 [Description]           TEXT                                        NULL,
 [IsBrowsable]           BIT                                     NOT NULL  CONSTRAINT [DF_ServerStaticOrMvcDefPathList_IsBowsable] DEFAULT ((0)),
 [IsStaticOrMvcDefOnly]  BIT                                     NOT NULL  CONSTRAINT [DF_ServerStaticOrMvcDefPathList_IsStaticOrMvcDefOnly] DEFAULT ((0)),
 [UserId]                INT                                     NOT NULL,
 [Active]                BIT                                     NOT NULL,
 [TimeStamp]             DATETIME2                               NOT NULL  CONSTRAINT [DF_ServerStaticOrMvcDefPathList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ServerStaticOrMvcDefPathList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ServerStaticOrMvcDefPathList]  UNIQUE      NONCLUSTERED ([SystemName] asc) ,
 CONSTRAINT [FK_ServerStaticOrMvcDefPathList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 