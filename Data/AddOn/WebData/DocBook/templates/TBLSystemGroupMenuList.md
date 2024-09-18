
 IF OBJECT_ID('[dbo].[SystemGroupMenuList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemGroupMenuList] 
 GO
 CREATE TABLE [dbo].[SystemGroupMenuList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [SystemName]   VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_SystemGroupMenuList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_SystemGroupMenuList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SystemGroupMenuList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SystemGroupMenuList]  UNIQUE      NONCLUSTERED ([SystemName] asc) ,
 CONSTRAINT [FK_SystemGroupMenuList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 