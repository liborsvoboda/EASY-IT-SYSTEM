
 IF OBJECT_ID('[dbo].[LicSrvUsedLicenseList]') IS NOT NULL 
 DROP TABLE [dbo].[LicSrvUsedLicenseList] 
 GO
 CREATE TABLE [dbo].[LicSrvUsedLicenseList] ( 
 [Id]             INT              IDENTITY(1,1)          NOT NULL,
 [AlgorithmName]  VARCHAR(30)                             NOT NULL,
 [PartNumber]     VARCHAR(50)                             NOT NULL,
 [UnlockCode]     VARCHAR(50)                             NOT NULL,
 [AddressId]      INT                                     NOT NULL,
 [ItemId]         INT                                     NOT NULL,
 [License]        VARCHAR(50)                             NOT NULL,
 [ActivateDate]   DATETIME2                               NOT NULL  CONSTRAINT [DF_UsedLicenceList_ActivateDate] DEFAULT (getdate()),
 [IpAddress]      VARCHAR(50)                             NOT NULL,
 CONSTRAINT   [PK_UsedLicenseList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_UsedLicenseList_AddressList] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[BusinessAddressList] (Id) ,
 CONSTRAINT [FK_UsedLicenseList_ItemList] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[BasicItemList] (Id) )
 
 