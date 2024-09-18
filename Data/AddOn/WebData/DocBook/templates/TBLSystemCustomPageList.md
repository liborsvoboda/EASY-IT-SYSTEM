
 IF OBJECT_ID('[dbo].[SystemCustomPageList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemCustomPageList] 
 GO
 CREATE TABLE [dbo].[SystemCustomPageList] ( 
 [Id]                INT              IDENTITY(1,1)          NOT NULL,
 [PageName]          VARCHAR(250)                            NOT NULL,
 [Description]       TEXT                                        NULL,
 [IsMultiFormType]   BIT                                     NOT NULL,
 [IsServerUrl]       BIT                                     NOT NULL,
 [StartupUrl]        VARCHAR(512)                                NULL,
 [IsWebServer]       BIT                                     NOT NULL,
 [StartupSubFolder]  VARCHAR(150)                                NULL,
 [StartupCommand]    VARCHAR(500)                                NULL,
 [IsGraphType]       BIT                                     NOT NULL,
 [Active]            BIT                                     NOT NULL,
 [UserId]            INT                                     NOT NULL,
 [TimeStamp]         DATETIME2                               NOT NULL  CONSTRAINT [DF_SystemCustomPageList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SystemCustomPageList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SystemCustomPageList]  UNIQUE      NONCLUSTERED ([PageName] asc) ,
 CONSTRAINT [FK_SystemCustomPageList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 