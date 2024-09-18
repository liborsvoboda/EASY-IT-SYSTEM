
 IF OBJECT_ID('[dbo].[SystemModuleList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemModuleList] 
 GO
 CREATE TABLE [dbo].[SystemModuleList] ( 
 [Id]               INT              IDENTITY(1,1)          NOT NULL,
 [ModuleType]       VARCHAR(50)                             NOT NULL,
 [Name]             VARCHAR(255)                            NOT NULL,
 [FolderPath]       VARCHAR(255)                                NULL,
 [FileName]         VARCHAR(255)                                NULL,
 [StartupCommand]   VARCHAR(255)                                NULL,
 [Description]      TEXT                                        NULL,
 [ForegroundColor]  VARCHAR(50)                             NOT NULL,
 [BackgroundColor]  VARCHAR(50)                             NOT NULL,
 [IconName]         VARCHAR(50)                             NOT NULL,
 [IconColor]        VARCHAR(50)                             NOT NULL,
 [UserId]           INT                                     NOT NULL,
 [TimeStamp]        DATETIME2                               NOT NULL  CONSTRAINT [DF_SystemModuleList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SystemModuleList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SystemModuleList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_SystemModuleList_SystemModuleList] FOREIGN KEY ([Id]) REFERENCES [dbo].[SystemModuleList] (Id) ,
 CONSTRAINT [FK_SystemModuleList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 