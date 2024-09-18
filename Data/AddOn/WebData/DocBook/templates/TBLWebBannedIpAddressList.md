
 IF OBJECT_ID('[dbo].[WebBannedIpAddressList]') IS NOT NULL 
 DROP TABLE [dbo].[WebBannedIpAddressList] 
 GO
 CREATE TABLE [dbo].[WebBannedIpAddressList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [IpAddress]    VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_WebBannedUserList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_WebBannedUserList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebBannedUserList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebBannedUserList]  UNIQUE      NONCLUSTERED ([IpAddress] asc) ,
 CONSTRAINT [FK_WebBannedUserList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 