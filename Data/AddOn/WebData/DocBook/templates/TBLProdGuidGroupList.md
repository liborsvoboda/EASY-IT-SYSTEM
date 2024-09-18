
 IF OBJECT_ID('[dbo].[ProdGuidGroupList]') IS NOT NULL 
 DROP TABLE [dbo].[ProdGuidGroupList] 
 GO
 CREATE TABLE [dbo].[ProdGuidGroupList] ( 
 [Id]         INT              IDENTITY(1,1)          NOT NULL,
 [Name]       VARCHAR(50)                             NOT NULL,
 [UserId]     INT                                     NOT NULL,
 [Timestamp]  DATETIME2                               NOT NULL,
 CONSTRAINT   [PK_ProdGuidGroupList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ProdGuidGroupList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_ProdGuidGroupList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 