
 IF OBJECT_ID('[dbo].[ProviderGeneratedToolList]') IS NOT NULL 
 DROP TABLE [dbo].[ProviderGeneratedToolList] 
 GO
 CREATE TABLE [dbo].[ProviderGeneratedToolList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Rating]       INT                                         NULL,
 [DescActive]   BIT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_GeneratedToolList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_GeneratedToolList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_GeneratedToolList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_GeneratedToolList] 
    ON [dbo].[ProviderGeneratedToolList] ([Name] asc, [UserId] asc)
 CREATE NONCLUSTERED INDEX [IX_GeneratedToolList_1] 
    ON [dbo].[ProviderGeneratedToolList] ([Name] asc)
 CREATE NONCLUSTERED INDEX [IX_GeneratedToolList_2] 
    ON [dbo].[ProviderGeneratedToolList] ([UserId] asc)