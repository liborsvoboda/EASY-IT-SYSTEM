
 IF OBJECT_ID('[dbo].[SolutionMessageTypeList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionMessageTypeList] 
 GO
 CREATE TABLE [dbo].[SolutionMessageTypeList] ( 
 [Id]             INT              IDENTITY(1,1)          NOT NULL,
 [Name]           VARCHAR(50)                             NOT NULL,
 [Variables]      TEXT                                        NULL,
 [AnswerAllowed]  BIT                                     NOT NULL  CONSTRAINT [DF_SolutionMessageTypeList_AnswerEnabled] DEFAULT ((0)),
 [IsSystemOnly]   BIT                                     NOT NULL  CONSTRAINT [DF_SolutionMessageTypeList_IsSystemOnly] DEFAULT ((0)),
 [Description]    TEXT                                        NULL,
 [UserId]         INT                                     NOT NULL,
 [TimeStamp]      DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionMessageTypeList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionMessageTypeList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SolutionMessageTypeList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_SolutionMessageTypeList_SolutionUserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 