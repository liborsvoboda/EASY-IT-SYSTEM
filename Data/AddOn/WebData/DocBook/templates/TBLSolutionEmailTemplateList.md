
 IF OBJECT_ID('[dbo].[SolutionEmailTemplateList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionEmailTemplateList] 
 GO
 CREATE TABLE [dbo].[SolutionEmailTemplateList] ( 
 [Id]                INT              IDENTITY(1,1)          NOT NULL,
 [SystemLanguageId]  INT                                     NOT NULL,
 [TemplateName]      VARCHAR(50)                             NOT NULL,
 [Variables]         TEXT                                    NOT NULL,
 [Subject]           VARCHAR(255)                            NOT NULL,
 [Email]             TEXT                                        NULL,
 [UserId]            INT                                     NOT NULL,
 [TimeStamp]         DATETIME2                               NOT NULL  CONSTRAINT [DF_EmailTemplateList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_EmailTemplateList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_EmailTemplateList]  UNIQUE      NONCLUSTERED ([SystemLanguageId] asc, [TemplateName] asc) ,
 CONSTRAINT [FK_EmailTemplateList_SystemLanguageList] FOREIGN KEY ([SystemLanguageId]) REFERENCES [dbo].[SolutionLanguageList] (Id) ,
 CONSTRAINT [FK_EmailTemplateList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 