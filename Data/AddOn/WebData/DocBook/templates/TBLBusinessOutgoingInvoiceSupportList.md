
 IF OBJECT_ID('[dbo].[BusinessOutgoingInvoiceSupportList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessOutgoingInvoiceSupportList] 
 GO
 CREATE TABLE [dbo].[BusinessOutgoingInvoiceSupportList] ( 
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
 [TimeStamp]          DATETIME2                               NOT NULL  CONSTRAINT [DF_OutgoingInvoiceItemList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_OutgoingInvoiceItemList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_OutgoingInvoiceItemList_OutgoingInvoiceList] FOREIGN KEY ([DocumentNumber]) REFERENCES [dbo].[BusinessOutgoingInvoiceList] (DocumentNumber)  ON DELETE CASCADE ,
 CONSTRAINT [FK_OutgoingInvoiceItemList_UnitList] FOREIGN KEY ([Unit]) REFERENCES [dbo].[BasicUnitList] (Name) ,
 CONSTRAINT [FK_OutgoingInvoiceItemList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_OutgoingInvoiceItemList] 
    ON [dbo].[BusinessOutgoingInvoiceSupportList] ([DocumentNumber] asc)