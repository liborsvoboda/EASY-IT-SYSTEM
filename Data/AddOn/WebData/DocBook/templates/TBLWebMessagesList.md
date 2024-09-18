
 IF OBJECT_ID('[dbo].[WebMessagesList]') IS NOT NULL 
 DROP TABLE [dbo].[WebMessagesList] 
 GO
 CREATE TABLE [dbo].[WebMessagesList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_WebMessagesList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_WebMessagesList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebMessagesList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebMessagesList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_WebMessagesList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 