
 IF OBJECT_ID('[dbo].[ServerCorsDefAllowedOriginList]') IS NOT NULL 
 DROP TABLE [dbo].[ServerCorsDefAllowedOriginList] 
 GO
 CREATE TABLE [dbo].[ServerCorsDefAllowedOriginList] ( 
 [Id]             INT              IDENTITY(1,1)          NOT NULL,
 [AllowedDomain]  VARCHAR(50)                             NOT NULL,
 [Description]    TEXT                                        NULL,
 [UserId]         INT                                     NOT NULL,
 [Active]         BIT                                     NOT NULL  CONSTRAINT [DF_ServerCorsDefAllowedOriginList_Active] DEFAULT ((1)),
 [TimeStamp]      DATETIME2                               NOT NULL  CONSTRAINT [DF_ServerCorsDefAllowedOriginList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ServerCorsDefAllowedOriginList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ServerCorsDefAllowedOriginList]  UNIQUE      NONCLUSTERED ([AllowedDomain] asc) ,
 CONSTRAINT [FK_ServerCorsDefAllowedOriginList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 