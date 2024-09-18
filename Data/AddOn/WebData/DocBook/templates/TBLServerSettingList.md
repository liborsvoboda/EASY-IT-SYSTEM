
 IF OBJECT_ID('[dbo].[ServerSettingList]') IS NOT NULL 
 DROP TABLE [dbo].[ServerSettingList] 
 GO
 CREATE TABLE [dbo].[ServerSettingList] ( 
 [Id]                  INT              IDENTITY(1,1)          NOT NULL,
 [InheritedGroupName]  VARCHAR(50)                             NOT NULL,
 [Type]                VARCHAR(50)                             NOT NULL  CONSTRAINT [DF_ServerSettingList_Type] DEFAULT ('bit'),
 [Key]                 NVARCHAR(150)                           NOT NULL,
 [Value]               NVARCHAR(150)                           NOT NULL,
 [Description]         TEXT                                        NULL,
 [Link]                VARCHAR(1024)                               NULL,
 [UserId]              INT                                     NOT NULL,
 [Active]              BIT                                     NOT NULL  CONSTRAINT [DF_AdminConfiguration_Active] DEFAULT ((1)),
 [Timestamp]           DATETIME2                               NOT NULL  CONSTRAINT [DF_AdminConfiguration_CreateDate] DEFAULT (getdate()),
 CONSTRAINT   [PK_AdminConfiguration]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ServerSettingList]  UNIQUE      NONCLUSTERED ([Key] asc) ,
 CONSTRAINT [FK_ServerSettingList_SolutionUserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 