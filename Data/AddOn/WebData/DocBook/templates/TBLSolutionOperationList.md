
 IF OBJECT_ID('[dbo].[SolutionOperationList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionOperationList] 
 GO
 CREATE TABLE [dbo].[SolutionOperationList] ( 
 [Id]                       INT              IDENTITY(1,1)          NOT NULL,
 [InheritedTypeName]        VARCHAR(50)                             NOT NULL,
 [Name]                     VARCHAR(255)                            NOT NULL,
 [InputData]                TEXT                                    NOT NULL,
 [InheritedResultTypeName]  VARCHAR(50)                             NOT NULL,
 [Description]              TEXT                                        NULL,
 [UserId]                   INT                                     NOT NULL,
 [Active]                   BIT                                     NOT NULL  CONSTRAINT [DF_SolutionOperationList_Active] DEFAULT ((1)),
 [TimeStamp]                DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionOperationList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionOperationList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SolutionOperationList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_SolutionOperationList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 