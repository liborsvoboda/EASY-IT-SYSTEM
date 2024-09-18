
 IF OBJECT_ID('[dbo].[WebUserSettingList]') IS NOT NULL 
 DROP TABLE [dbo].[WebUserSettingList] 
 GO
 CREATE TABLE [dbo].[WebUserSettingList] ( 
 [Id]         INT              IDENTITY(1,1)          NOT NULL,
 [Key]        NVARCHAR(50)                            NOT NULL,
 [Value]      NVARCHAR(250)                           NOT NULL,
 [UserId]     INT                                     NOT NULL,
 [Timestamp]  DATETIME2                               NOT NULL  CONSTRAINT [DF_WebUserSettingList_CreateDate] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebUserSettingList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebUserSettingList]  UNIQUE      NONCLUSTERED ([UserId] asc, [Key] asc) ,
 CONSTRAINT [FK_WebUserSettingList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id)  ON DELETE CASCADE )
 
 