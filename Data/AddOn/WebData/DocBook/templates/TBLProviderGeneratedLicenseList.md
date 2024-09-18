
 IF OBJECT_ID('[dbo].[ProviderGeneratedLicenseList]') IS NOT NULL 
 DROP TABLE [dbo].[ProviderGeneratedLicenseList] 
 GO
 CREATE TABLE [dbo].[ProviderGeneratedLicenseList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [PartNumber]   VARCHAR(50)                             NOT NULL,
 [Type]         VARCHAR(50)                             NOT NULL,
 [Expiration]   DATETIME2                               NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_ProviderGeneratedLicenseList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ProviderGeneratedLicenseList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ProviderGeneratedLicenseList]  UNIQUE      NONCLUSTERED ([PartNumber] asc) ,
 CONSTRAINT [FK_ProviderGeneratedLicenseList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 