
 IF OBJECT_ID('[dbo].[BusinessIncomingInvoiceSupportList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessIncomingInvoiceSupportList] 
 GO
 CREATE TABLE [dbo].[BusinessIncomingInvoiceSupportList] ( 
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
 [TimeStamp]          DATETIME2                               NOT NULL  CONSTRAINT [DF_IncomingInvoiceItemList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_IncomingInvoiceItemList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_IncomingInvoiceItemList_IncomingInvoiceList] FOREIGN KEY ([DocumentNumber]) REFERENCES [dbo].[BusinessIncomingInvoiceList] (DocumentNumber)  ON DELETE CASCADE ,
 CONSTRAINT [FK_IncomingInvoiceItemList_UnitList] FOREIGN KEY ([Unit]) REFERENCES [dbo].[BasicUnitList] (Name) ,
 CONSTRAINT [FK_IncomingInvoiceItemList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_IncomingInvoiceItemList] 
    ON [dbo].[BusinessIncomingInvoiceSupportList] ([DocumentNumber] asc)