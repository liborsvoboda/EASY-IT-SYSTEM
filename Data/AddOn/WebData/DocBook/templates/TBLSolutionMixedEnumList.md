
 IF OBJECT_ID('[dbo].[SolutionMixedEnumList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionMixedEnumList] 
 GO
 CREATE TABLE [dbo].[SolutionMixedEnumList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [ItemsGroup]   VARCHAR(50)                             NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_GlobalMixedEnumList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_GlobalMixedEnumList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_GlobalMixedEnumList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_GlobalMixedEnumList]  UNIQUE      NONCLUSTERED ([ItemsGroup] asc, [Name] asc) ,
 CONSTRAINT [FK_GlobalMixedEnumList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) ,
 CONSTRAINT [FK_SolutionMixedEnumList_SolutionMixedEnumList] FOREIGN KEY ([Id]) REFERENCES [dbo].[SolutionMixedEnumList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_SolutionMixedEnumList] 
    ON [dbo].[SolutionMixedEnumList] ([ItemsGroup] asc)
 CREATE NONCLUSTERED INDEX [IX_SolutionMixedEnumList_1] 
    ON [dbo].[SolutionMixedEnumList] ([Name] asc)