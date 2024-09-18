
 IF OBJECT_ID('[dbo].[BasicItemList]') IS NOT NULL 
 DROP TABLE [dbo].[BasicItemList] 
 GO
 CREATE TABLE [dbo].[BasicItemList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [PartNumber]   VARCHAR(50)                             NOT NULL,
 [Name]         VARCHAR(150)                            NOT NULL,
 [Description]  TEXT                                        NULL,
 [Unit]         VARCHAR(10)                             NOT NULL,
 [Price]        NUMERIC(10,2)                           NOT NULL,
 [VatId]        INT                                     NOT NULL,
 [CurrencyId]   INT                                     NOT NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_ItemList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_ItemList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ItemList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ItemList]  UNIQUE      NONCLUSTERED ([PartNumber] asc) ,
 CONSTRAINT [FK_ItemList_CurrencyList] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[BasicCurrencyList] (Id) ,
 CONSTRAINT [FK_ItemList_UnitList] FOREIGN KEY ([Unit]) REFERENCES [dbo].[BasicUnitList] (Name) ,
 CONSTRAINT [FK_ItemList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) ,
 CONSTRAINT [FK_ItemList_VatList] FOREIGN KEY ([VatId]) REFERENCES [dbo].[BasicVatList] (Id) )
 
 