
 IF OBJECT_ID('[dbo].[BasicAttachmentList]') IS NOT NULL 
 DROP TABLE [dbo].[BasicAttachmentList] 
 GO
 CREATE TABLE [dbo].[BasicAttachmentList] ( 
 [Id]          INT              IDENTITY(1,1)          NOT NULL,
 [ParentId]    INT                                     NOT NULL,
 [ParentType]  VARCHAR(20)                             NOT NULL,
 [FileName]    VARCHAR(150)                            NOT NULL,
 [Attachment]  VARBINARY(max)                          NOT NULL,
 [UserId]      INT                                     NOT NULL,
 [TimeStamp]   DATETIME2                               NOT NULL  CONSTRAINT [DF_AttachmentList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_AttachmentList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [UX_AttachmentList]  UNIQUE      NONCLUSTERED ([ParentId] asc, [FileName] asc) ,
 CONSTRAINT [FK_AttachmentList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_AttachmentList] 
    ON [dbo].[BasicAttachmentList] ([ParentId] asc, [ParentType] asc)