
 IF OBJECT_ID('[dbo].[SolutionMessageModuleList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionMessageModuleList] 
 GO
 CREATE TABLE [dbo].[SolutionMessageModuleList] ( 
 [Id]               INT              IDENTITY(1,1)          NOT NULL,
 [Level]            INT                                     NOT NULL  CONSTRAINT [DF_SolutionMessageModuleList_Level] DEFAULT ((0)),
 [MessageParentId]  INT                                         NULL,
 [MessageTypeId]    INT                                     NOT NULL,
 [Subject]          VARCHAR(150)                            NOT NULL,
 [HtmlMessage]      TEXT                                    NOT NULL,
 [Shown]            BIT                                     NOT NULL,
 [Archived]         BIT                                     NOT NULL,
 [IsSystemMessage]  BIT                                     NOT NULL,
 [Published]        BIT                                     NOT NULL  CONSTRAINT [DF_SolutionMessageModuleList_Publish] DEFAULT ((0)),
 [FromUserId]       INT                                         NULL,
 [ToUserId]         INT                                         NULL,
 [TimeStamp]        DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionMessageModuleList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionMessageModuleList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SolutionMessageModuleList]  UNIQUE      NONCLUSTERED ([Subject] asc) ,
 CONSTRAINT [FK_SolutionMessageModuleList_SolutionMessageModuleListParent] FOREIGN KEY ([MessageParentId]) REFERENCES [dbo].[SolutionMessageModuleList] (Id) ,
 CONSTRAINT [FK_SolutionMessageModuleList_SolutionMessageTypeList] FOREIGN KEY ([MessageTypeId]) REFERENCES [dbo].[SolutionMessageTypeList] (Id) ,
 CONSTRAINT [FK_SolutionMessageModuleList_SolutionUserListFrom] FOREIGN KEY ([FromUserId]) REFERENCES [dbo].[SolutionUserList] (Id) ,
 CONSTRAINT [FK_SolutionMessageModuleList_SolutionUserListTo] FOREIGN KEY ([ToUserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 