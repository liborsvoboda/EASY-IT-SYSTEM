
 IF OBJECT_ID('[dbo].[SolutionTaskList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionTaskList] 
 GO
 CREATE TABLE [dbo].[SolutionTaskList] ( 
 [Id]                   INT              IDENTITY(1,1)          NOT NULL,
 [InheritedTargetType]  VARCHAR(50)                             NOT NULL,
 [InheritedStatusType]  VARCHAR(50)                             NOT NULL,
 [Message]              TEXT                                    NOT NULL,
 [Documentation]        TEXT                                    NOT NULL,
 [ImageName]            VARCHAR(150)                                NULL,
 [Image]                VARBINARY(max)                              NULL,
 [AttachmentName]       VARCHAR(150)                                NULL,
 [Attachment]           VARBINARY(max)                              NULL,
 [UserId]               INT                                     NOT NULL,
 [TimeStamp]            DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionTaskList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionTaskList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_SolutionTaskList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_SolutionTaskList] 
    ON [dbo].[SolutionTaskList] ([InheritedTargetType] asc)
 CREATE NONCLUSTERED INDEX [IX_SolutionTaskList_1] 
    ON [dbo].[SolutionTaskList] ([InheritedTargetType] asc, [InheritedStatusType] asc)
 CREATE NONCLUSTERED INDEX [IX_SolutionTaskList_2] 
    ON [dbo].[SolutionTaskList] ([InheritedStatusType] asc)