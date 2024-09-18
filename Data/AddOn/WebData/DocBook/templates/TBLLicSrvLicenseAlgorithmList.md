
 IF OBJECT_ID('[dbo].[LicSrvLicenseAlgorithmList]') IS NOT NULL 
 DROP TABLE [dbo].[LicSrvLicenseAlgorithmList] 
 GO
 CREATE TABLE [dbo].[LicSrvLicenseAlgorithmList] ( 
 [Id]               INT              IDENTITY(1,1)          NOT NULL,
 [AddressId]        INT                                     NOT NULL,
 [ItemId]           INT                                     NOT NULL,
 [Name]             VARCHAR(30)                             NOT NULL,
 [ValidFrom]        DATE                                        NULL,
 [ValidTo]          DATE                                        NULL,
 [Algorithm]        VARCHAR(2000)                           NOT NULL,
 [Description]      TEXT                                        NULL,
 [LimitActive]      BIT                                     NOT NULL,
 [ActivationLimit]  INT                                         NULL,
 [UsedCount]        INT                                     NOT NULL,
 [Active]           BIT                                     NOT NULL,
 [UserId]           INT                                     NOT NULL,
 [TimeStamp]        DATETIME2                               NOT NULL  CONSTRAINT [DF_LicSrvLicenseAlgorithmList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_LicSrvLicenseAlgorithmList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_LicSrvLicenseAlgorithmList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_LicenseAlgorithmList_AddressList] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[BusinessAddressList] (Id) ,
 CONSTRAINT [FK_LicenseAlgorithmList_ItemList] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[BasicItemList] (Id) ,
 CONSTRAINT [FK_LicenseAlgorithmList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 