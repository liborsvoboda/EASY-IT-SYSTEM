
 IF OBJECT_ID('[dbo].[ServerLiveDataMonitorList]') IS NOT NULL 
 DROP TABLE [dbo].[ServerLiveDataMonitorList] 
 GO
 CREATE TABLE [dbo].[ServerLiveDataMonitorList] ( 
 [Id]              INT              IDENTITY(1,1)          NOT NULL,
 [RootPath]        VARCHAR(1024)                           NOT NULL,
 [FileExtensions]  VARCHAR(1024)                           NOT NULL,
 [Description]     TEXT                                        NULL,
 [UserId]          INT                                     NOT NULL,
 [Active]          BIT                                     NOT NULL  CONSTRAINT [DF_ServerLiveDataMonitorList_Active] DEFAULT ((1)),
 [TimeStamp]       DATETIME2                               NOT NULL  CONSTRAINT [DF_ServerLiveDataMonitorList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ServerLiveDataMonitorList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ServerLiveDataMonitorList]  UNIQUE      NONCLUSTERED ([RootPath] asc) ,
 CONSTRAINT [FK_ServerLiveDataMonitorList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 