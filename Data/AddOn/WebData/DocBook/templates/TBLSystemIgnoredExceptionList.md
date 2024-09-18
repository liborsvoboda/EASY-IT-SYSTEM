
 IF OBJECT_ID('[dbo].[SystemIgnoredExceptionList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemIgnoredExceptionList] 
 GO
 CREATE TABLE [dbo].[SystemIgnoredExceptionList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [ErrorNumber]  VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_IgnoredExceptionList_Active] DEFAULT ((1)),
 [UserId]       INT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_IgnoredExceptionList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_IgnoredExceptionList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_IgnoredExceptionList]  UNIQUE      NONCLUSTERED ([ErrorNumber] asc) ,
 CONSTRAINT [FK_IgnoredExceptionList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 