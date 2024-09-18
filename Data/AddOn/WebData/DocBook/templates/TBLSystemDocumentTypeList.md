
 IF OBJECT_ID('[dbo].[SystemDocumentTypeList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemDocumentTypeList] 
 GO
 CREATE TABLE [dbo].[SystemDocumentTypeList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [SystemName]   VARCHAR(50)                             NOT NULL  CONSTRAINT [DF_DocumentTypeList_SystemName] DEFAULT ('MustProgramming'),
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Timestamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_DocumentTypeList_Timestamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_DocumentTypeList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_DocumentTypeList]  UNIQUE      NONCLUSTERED ([SystemName] asc) ,
 CONSTRAINT [FK_DocumentTypeList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 