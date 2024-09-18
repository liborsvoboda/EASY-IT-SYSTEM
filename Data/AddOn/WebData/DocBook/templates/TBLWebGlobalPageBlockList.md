
 IF OBJECT_ID('[dbo].[WebGlobalPageBlockList]') IS NOT NULL 
 DROP TABLE [dbo].[WebGlobalPageBlockList] 
 GO
 CREATE TABLE [dbo].[WebGlobalPageBlockList] ( 
 [Id]                   INT              IDENTITY(1,1)          NOT NULL,
 [PagePartType]         VARCHAR(50)                             NOT NULL,
 [Sequence]             INT                                     NOT NULL,
 [Name]                 VARCHAR(50)                             NOT NULL,
 [Description]          TEXT                                        NULL,
 [RewriteLowerLevel]    BIT                                     NOT NULL,
 [GuestHtmlContent]     TEXT                                        NULL,
 [UserHtmlContent]      TEXT                                        NULL,
 [AdminHtmlContent]     TEXT                                        NULL,
 [ProviderHtmlContent]  TEXT                                        NULL,
 [Active]               BIT                                     NOT NULL,
 [UserId]               INT                                     NOT NULL,
 [TimeStamp]            DATETIME2                               NOT NULL  CONSTRAINT [DF_WebGlobalBodyBlockList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebGlobalBodyBlockList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebGlobalBodyBlockList]  UNIQUE      NONCLUSTERED ([Name] asc, [PagePartType] asc) ,
 CONSTRAINT [FK_WebGlobalBodyBlockList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 