
 IF OBJECT_ID('[dbo].[WebCoreFileList]') IS NOT NULL 
 DROP TABLE [dbo].[WebCoreFileList] 
 GO
 CREATE TABLE [dbo].[WebCoreFileList] ( 
 [Id]                 INT              IDENTITY(1,1)          NOT NULL,
 [SpecificationType]  VARCHAR(50)                             NOT NULL,
 [Sequence]           INT                                     NOT NULL,
 [MetroPath]          VARCHAR(100)                            NOT NULL  CONSTRAINT [DF_WebCoreFileList_MetroPath] DEFAULT (''),
 [FileName]           VARCHAR(50)                             NOT NULL,
 [Description]        TEXT                                        NULL,
 [RewriteLowerLevel]  BIT                                     NOT NULL  CONSTRAINT [DF_WebCoreFileList_RewriteLowerLevel] DEFAULT ((0)),
 [GuestFileContent]   TEXT                                        NULL,
 [UserFileContent]    TEXT                                        NULL,
 [AdminFileContent]   TEXT                                        NULL,
 [ProviderContent]    TEXT                                        NULL,
 [IsUniquePath]       BIT                                     NOT NULL  CONSTRAINT [DF_WebCoreFileList_IsUniquePath] DEFAULT ((0)),
 [AutoUpdateOnSave]   BIT                                     NOT NULL,
 [Active]             BIT                                     NOT NULL,
 [UserId]             INT                                     NOT NULL,
 [TimeStamp]          DATETIME2                               NOT NULL  CONSTRAINT [DF_WebCoreFileList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebCoreFileList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebCoreFileList]  UNIQUE      NONCLUSTERED ([FileName] asc, [SpecificationType] asc) ,
 CONSTRAINT [FK_WebCoreFileList_GlobalUserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 