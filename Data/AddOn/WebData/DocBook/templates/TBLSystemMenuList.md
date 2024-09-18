
 IF OBJECT_ID('[dbo].[SystemMenuList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemMenuList] 
 GO
 CREATE TABLE [dbo].[SystemMenuList] ( 
 [Id]             INT              IDENTITY(1,1)          NOT NULL,
 [MenuType]       VARCHAR(50)                             NOT NULL,
 [GroupId]        INT                                     NOT NULL,
 [FormPageName]   VARCHAR(250)                            NOT NULL,
 [AccessRole]     VARCHAR(1024)                           NOT NULL,
 [Description]    TEXT                                        NULL,
 [UserId]         INT                                     NOT NULL,
 [NotShowInMenu]  BIT                                     NOT NULL  CONSTRAINT [DF_SystemMenuList_NotShowInMenu] DEFAULT ((0)),
 [Active]         BIT                                     NOT NULL  CONSTRAINT [DF_SystemMenuList_Active] DEFAULT ((1)),
 [TimeStamp]      DATETIME2                               NOT NULL  CONSTRAINT [DF_SystemMenuList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SystemMenuList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_GlobalMenuList]  UNIQUE      NONCLUSTERED ([FormPageName] asc) ,
 CONSTRAINT [FK_SystemMenuList_SystemGroupMenuList] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SystemGroupMenuList] (Id) ,
 CONSTRAINT [FK_SystemMenuList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 