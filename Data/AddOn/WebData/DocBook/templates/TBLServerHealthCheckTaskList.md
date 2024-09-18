
 IF OBJECT_ID('[dbo].[ServerHealthCheckTaskList]') IS NOT NULL 
 DROP TABLE [dbo].[ServerHealthCheckTaskList] 
 GO
 CREATE TABLE [dbo].[ServerHealthCheckTaskList] ( 
 [Id]               INT              IDENTITY(1,1)          NOT NULL,
 [TaskName]         VARCHAR(100)                            NOT NULL,
 [Type]             VARCHAR(50)                             NOT NULL,
 [ServerDrive]      VARCHAR(50)                                 NULL,
 [FolderPath]       VARCHAR(1024)                               NULL,
 [DbSqlConnection]  VARCHAR(1024)                               NULL,
 [IpAddress]        VARCHAR(20)                                 NULL,
 [ServerUrlPath]    VARCHAR(1024)                               NULL,
 [UrlPath]          VARCHAR(1024)                               NULL,
 [SizeMB]           INT                                         NULL,
 [Port]             INT                                         NULL,
 [UserId]           INT                                     NOT NULL,
 [Active]           BIT                                     NOT NULL  CONSTRAINT [DF_HealthCheckTaskList_Active] DEFAULT ((1)),
 [Timestamp]        DATETIME2                               NOT NULL  CONSTRAINT [DF_HealthCheckTaskList_Timestamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_HealthCheckTaskList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_HealthCheckTaskList]  UNIQUE      NONCLUSTERED ([TaskName] asc) ,
 CONSTRAINT [FK_HealthCheckTaskList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 