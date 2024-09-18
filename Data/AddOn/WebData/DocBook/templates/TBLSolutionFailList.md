
 IF OBJECT_ID('[dbo].[SolutionFailList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionFailList] 
 GO
 CREATE TABLE [dbo].[SolutionFailList] ( 
 [Id]              INT              IDENTITY(1,1)          NOT NULL,
 [Source]          VARCHAR(50)                             NOT NULL,
 [UserName]        VARCHAR(50)                                 NULL,
 [LogLevel]        VARCHAR(20)                                 NULL,
 [Message]         TEXT                                    NOT NULL,
 [ImageName]       VARCHAR(150)                                NULL,
 [Image]           VARBINARY(max)                              NULL,
 [AttachmentName]  VARCHAR(150)                                NULL,
 [Attachment]      VARBINARY(max)                              NULL,
 [UserId]          INT                                         NULL,
 [TimeStamp]       DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionFailList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionFailList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_SolutionFailList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 