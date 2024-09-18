
 IF OBJECT_ID('[dbo].[BusinessReceiptSupportList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessReceiptSupportList] 
 GO
 CREATE TABLE [dbo].[BusinessReceiptSupportList] ( 
 [Id]                 INT              IDENTITY(1,1)          NOT NULL,
 [DocumentNumber]     VARCHAR(20)                             NOT NULL,
 [PartNumber]         VARCHAR(50)                                 NULL,
 [Name]               VARCHAR(150)                            NOT NULL,
 [Unit]               VARCHAR(10)                             NOT NULL,
 [PcsPrice]           NUMERIC(10,2)                           NOT NULL,
 [Count]              NUMERIC(10,2)                           NOT NULL,
 [TotalPrice]         NUMERIC(10,2)                           NOT NULL,
 [Vat]                NUMERIC(10,2)                           NOT NULL,
 [TotalPriceWithVat]  NUMERIC(10,2)                           NOT NULL,
 [UserId]             INT                                     NOT NULL,
 [TimeStamp]          DATETIME2                               NOT NULL  CONSTRAINT [DF_ReceiptItemList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ReceiptItemList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_ReceiptItemList_ReceiptList] FOREIGN KEY ([DocumentNumber]) REFERENCES [dbo].[BusinessReceiptList] (DocumentNumber)  ON DELETE CASCADE ,
 CONSTRAINT [FK_ReceiptItemList_UnitList] FOREIGN KEY ([Unit]) REFERENCES [dbo].[BasicUnitList] (Name) ,
 CONSTRAINT [FK_ReceiptItemList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 