
 IF OBJECT_ID('[dbo].[BusinessOutgoingInvoiceList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessOutgoingInvoiceList] 
 GO
 CREATE TABLE [dbo].[BusinessOutgoingInvoiceList] ( 
 [Id]                 INT              IDENTITY(1,1)          NOT NULL,
 [DocumentNumber]     VARCHAR(20)                             NOT NULL,
 [Supplier]           VARCHAR(512)                            NOT NULL,
 [Customer]           VARCHAR(512)                            NOT NULL,
 [IssueDate]          DATETIME2                               NOT NULL,
 [TaxDate]            DATETIME2                               NOT NULL,
 [MaturityDate]       DATETIME2                               NOT NULL,
 [PaymentMethodId]    INT                                     NOT NULL,
 [MaturityId]         INT                                     NOT NULL,
 [OrderNumber]        VARCHAR(50)                                 NULL,
 [Storned]            BIT                                     NOT NULL,
 [PaymentStatusId]    INT                                     NOT NULL,
 [TotalCurrencyId]    INT                                     NOT NULL,
 [Description]        TEXT                                        NULL,
 [TotalPriceWithVat]  NUMERIC(10,2)                           NOT NULL,
 [ReceiptId]          INT                                         NULL,
 [CreditNoteId]       INT                                         NULL,
 [UserId]             INT                                     NOT NULL,
 [TimeStamp]          DATETIME2                               NOT NULL  CONSTRAINT [DF_OutgoingInvoiceList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_OutgoingInvoiceList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_OutgoingInvoiceList]  UNIQUE      NONCLUSTERED ([DocumentNumber] asc) ,
 CONSTRAINT [FK_OutgoingInvoiceList_CreditNoteList] FOREIGN KEY ([CreditNoteId]) REFERENCES [dbo].[BusinessCreditNoteList] (Id)  ON DELETE SET NULL ,
 CONSTRAINT [FK_OutgoingInvoiceList_CurrencyList] FOREIGN KEY ([TotalCurrencyId]) REFERENCES [dbo].[BasicCurrencyList] (Id) ,
 CONSTRAINT [FK_OutgoingInvoiceList_MaturityList] FOREIGN KEY ([MaturityId]) REFERENCES [dbo].[BusinessMaturityList] (Id) ,
 CONSTRAINT [FK_OutgoingInvoiceList_PaymentMethodList] FOREIGN KEY ([PaymentMethodId]) REFERENCES [dbo].[BusinessPaymentMethodList] (Id) ,
 CONSTRAINT [FK_OutgoingInvoiceList_PaymentStatusList] FOREIGN KEY ([PaymentStatusId]) REFERENCES [dbo].[BusinessPaymentStatusList] (Id) ,
 CONSTRAINT [FK_OutgoingInvoiceList_ReceiptList] FOREIGN KEY ([ReceiptId]) REFERENCES [dbo].[BusinessReceiptList] (Id)  ON DELETE SET NULL ,
 CONSTRAINT [FK_OutgoingInvoiceList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_OutgoingInvoiceList_Customer] 
    ON [dbo].[BusinessOutgoingInvoiceList] ([Customer] asc)