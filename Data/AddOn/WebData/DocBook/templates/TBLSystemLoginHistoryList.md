
 IF OBJECT_ID('[dbo].[SystemLoginHistoryList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemLoginHistoryList] 
 GO
 CREATE TABLE [dbo].[SystemLoginHistoryList] ( 
 [Id]         INT              IDENTITY(1,1)          NOT NULL,
 [IpAddress]  VARCHAR(50)                             NOT NULL,
 [UserId]     INT                                     NOT NULL  CONSTRAINT [DF_LoginHistoryList_UserId] DEFAULT ((0)),
 [UserName]   VARCHAR(150)                            NOT NULL,
 [Timestamp]  DATETIME2                               NOT NULL  CONSTRAINT [DF_LoginHistory_timestamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_LoginHistory]  PRIMARY KEY CLUSTERED    ([Id] asc) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_LoginHistoryList] 
    ON [dbo].[SystemLoginHistoryList] ([IpAddress] asc)
 CREATE NONCLUSTERED INDEX [IX_LoginHistoryList_1] 
    ON [dbo].[SystemLoginHistoryList] ([UserId] asc)