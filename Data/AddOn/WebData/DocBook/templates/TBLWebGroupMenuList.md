
 IF OBJECT_ID('[dbo].[WebGroupMenuList]') IS NOT NULL 
 DROP TABLE [dbo].[WebGroupMenuList] 
 GO
 CREATE TABLE [dbo].[WebGroupMenuList] ( 
 [Id]         INT              IDENTITY(1,1)          NOT NULL,
 [Sequence]   INT                                     NOT NULL,
 [Onclick]    VARCHAR(255)                                NULL,
 [Name]       VARCHAR(50)                             NOT NULL,
 [UserId]     INT                                     NOT NULL,
 [Active]     BIT                                     NOT NULL,
 [TimeStamp]  DATETIME2                               NOT NULL  CONSTRAINT [DF_WebGroupMenuList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebGroupMenuList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebGroupMenuList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_WebGroupMenuList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 