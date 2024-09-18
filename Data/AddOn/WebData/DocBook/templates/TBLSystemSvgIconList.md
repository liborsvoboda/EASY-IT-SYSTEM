
 IF OBJECT_ID('[dbo].[SystemSvgIconList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemSvgIconList] 
 GO
 CREATE TABLE [dbo].[SystemSvgIconList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [SvgIconPath]  VARCHAR(4096)                           NOT NULL,
 [UserId]       INT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_SvgIconList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SvgIconList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SvgIconList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_SvgIconList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 