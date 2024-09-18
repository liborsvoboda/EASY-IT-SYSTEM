
 IF OBJECT_ID('[dbo].[ServerApiSecurityList]') IS NOT NULL 
 DROP TABLE [dbo].[ServerApiSecurityList] 
 GO
 CREATE TABLE [dbo].[ServerApiSecurityList] ( 
 [Id]                     INT              IDENTITY(1,1)          NOT NULL,
 [InheritedApiType]       VARCHAR(50)                             NOT NULL,
 [Name]                   VARCHAR(50)                             NOT NULL,
 [Description]            TEXT                                        NULL,
 [UrlSubPath]             VARCHAR(100)                                NULL,
 [WriteAllowedRoles]      VARCHAR(500)                                NULL,
 [ReadAllowedRoles]       VARCHAR(500)                                NULL,
 [WriteRestrictedAccess]  BIT                                     NOT NULL,
 [ReadRestrictedAccess]   BIT                                     NOT NULL  CONSTRAINT [DF_ServerApiSecurityList_ReadRestrictedAccess] DEFAULT ((0)),
 [RedirectPathOnError]    VARCHAR(100)                                NULL,
 [Active]                 BIT                                     NOT NULL  CONSTRAINT [DF_ServerApiSecurityList_Active] DEFAULT ((1)),
 [UserId]                 INT                                     NOT NULL,
 [TimeStamp]              DATETIME2                               NOT NULL  CONSTRAINT [DF_ServerApiSecurityList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ServerApiSecurityList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ServerApiSecurityList_2]  UNIQUE      NONCLUSTERED ([UrlSubPath] asc) ,
 CONSTRAINT   [IX_ServerApiSecurityList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_ServerApiSecurityList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 