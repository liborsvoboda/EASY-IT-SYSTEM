
 IF OBJECT_ID('[dbo].[WebSettingList]') IS NOT NULL 
 DROP TABLE [dbo].[WebSettingList] 
 GO
 CREATE TABLE [dbo].[WebSettingList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Key]          NVARCHAR(50)                            NOT NULL,
 [Value]        TEXT                                    NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Timestamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_WebSettingList_CreateDate] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebSettingList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebSettingList]  UNIQUE      NONCLUSTERED ([Key] asc) ,
 CONSTRAINT [FK_WebSettingList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id)  ON DELETE CASCADE )
 
 