
 IF OBJECT_ID('[dbo].[ProviderAutoGenServerReqList]') IS NOT NULL 
 DROP TABLE [dbo].[ProviderAutoGenServerReqList] 
 GO
 CREATE TABLE [dbo].[ProviderAutoGenServerReqList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [IpAddress]    VARCHAR(20)                             NOT NULL,
 [Name]         VARCHAR(100)                            NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                         NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_ProviderAutoGenServerCreateRequest_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ProviderAutoGenServerCreateRequest]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ProviderAutoGenServerCreateRequest]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_ProviderAutoGenServerCreateRequest_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 