
 IF OBJECT_ID('[dbo].[TemplateList]') IS NOT NULL 
 DROP TABLE [dbo].[TemplateList] 
 GO
 CREATE TABLE [dbo].[TemplateList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [GroupId]      INT                                     NOT NULL,
 [Sequence]     INT                                     NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Default]      BIT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_TemplateList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_TemplateList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_TemplateList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_TemplateList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_TemplateList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) ,
 CONSTRAINT [FK_TemplateList_UserRoleList] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SolutionUserRoleList] (Id) )
 
 