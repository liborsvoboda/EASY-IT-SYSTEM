
 IF OBJECT_ID('[dbo].[ProdGuidWorkList]') IS NOT NULL 
 DROP TABLE [dbo].[ProdGuidWorkList] 
 GO
 CREATE TABLE [dbo].[ProdGuidWorkList] ( 
 [Id]               INT              IDENTITY(1,1)          NOT NULL,
 [Date]             DATETIME2                               NOT NULL,
 [PersonalNumber]   INT                                     NOT NULL,
 [WorkPlace]        INT                                     NOT NULL,
 [OperationNumber]  INT                                     NOT NULL,
 [WorkTime]         TIME                                    NOT NULL,
 [Pcs]              INT                                     NOT NULL,
 [Amount]           NUMERIC(10,2)                           NOT NULL,
 [WorkPower]        NUMERIC(10,2)                           NOT NULL,
 [UserId]           INT                                     NOT NULL,
 [Timestamp]        DATETIME2                               NOT NULL,
 CONSTRAINT   [PK_ProdGuidWorkList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_ProdGuidWorkList_ProdGuidPersonList] FOREIGN KEY ([PersonalNumber]) REFERENCES [dbo].[ProdGuidPersonList] (PersonalNumber) ,
 CONSTRAINT [FK_ProdGuidWorkList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 