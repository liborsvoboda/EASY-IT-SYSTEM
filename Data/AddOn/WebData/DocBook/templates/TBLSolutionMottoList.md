
 IF OBJECT_ID('[dbo].[SolutionMottoList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionMottoList] 
 GO
 CREATE TABLE [dbo].[SolutionMottoList] ( 
 [Id]          INT              IDENTITY(1,1)          NOT NULL,
 [SystemName]  VARCHAR(50)                             NOT NULL,
 [UserId]      INT                                     NOT NULL  CONSTRAINT [DF_MottoList_UserId] DEFAULT ((1)),
 [Timestamp]   DATETIME2                               NOT NULL  CONSTRAINT [DF_MottoList_Timestamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_MottoList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_MottoList]  UNIQUE      NONCLUSTERED ([SystemName] asc) ,
 CONSTRAINT [FK_MottoList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 