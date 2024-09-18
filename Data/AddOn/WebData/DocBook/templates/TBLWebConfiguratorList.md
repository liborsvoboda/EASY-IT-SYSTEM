
 IF OBJECT_ID('[dbo].[WebConfiguratorList]') IS NOT NULL 
 DROP TABLE [dbo].[WebConfiguratorList] 
 GO
 CREATE TABLE [dbo].[WebConfiguratorList] ( 
 [Id]               INT              IDENTITY(1,1)          NOT NULL,
 [Name]             VARCHAR(50)                             NOT NULL,
 [IsStartupPage]    BIT                                     NOT NULL,
 [Description]      VARCHAR(max)                                NULL,
 [HtmlContent]      VARCHAR(max)                                NULL,
 [ServerUrl]        VARCHAR(500)                                NULL,
 [AuthRole]         VARCHAR(200)                                NULL,
 [AuthIgnore]       BIT                                     NOT NULL,
 [AuthRedirect]     BIT                                     NOT NULL,
 [AuthRedirectUrl]  VARCHAR(500)                                NULL,
 [IncludedIdList]   VARCHAR(500)                                NULL,
 [Active]           BIT                                     NOT NULL  CONSTRAINT [DF_WebConfiguratorList_Active] DEFAULT ((1)),
 [UserId]           INT                                     NOT NULL,
 [TimeStamp]        DATETIME2                               NOT NULL  CONSTRAINT [DF_WebConfiguratorList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebConfiguratorList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebConfiguratorList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT   [IX_WebConfiguratorList_1]  UNIQUE      NONCLUSTERED ([ServerUrl] asc) ,
 CONSTRAINT [FK_WebConfiguratorList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 