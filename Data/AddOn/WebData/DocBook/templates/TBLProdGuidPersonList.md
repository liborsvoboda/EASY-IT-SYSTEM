
 IF OBJECT_ID('[dbo].[ProdGuidPersonList]') IS NOT NULL 
 DROP TABLE [dbo].[ProdGuidPersonList] 
 GO
 CREATE TABLE [dbo].[ProdGuidPersonList] ( 
 [Id]              INT              IDENTITY(1,1)          NOT NULL,
 [GroupId]         INT                                     NOT NULL,
 [PersonalNumber]  INT                                     NOT NULL,
 [Name]            VARCHAR(50)                             NOT NULL,
 [SurName]         VARCHAR(100)                            NOT NULL,
 [UserId]          INT                                     NOT NULL,
 [Timestamp]       DATETIME2                               NOT NULL,
 CONSTRAINT   [PK_ProdGuidPersonList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ProdGuidPersonList]  UNIQUE      NONCLUSTERED ([PersonalNumber] asc) ,
 CONSTRAINT [FK_ProdGuidPersonList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 