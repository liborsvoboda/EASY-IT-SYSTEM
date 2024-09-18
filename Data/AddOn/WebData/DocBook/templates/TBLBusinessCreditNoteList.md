
 IF OBJECT_ID('[dbo].[BusinessCreditNoteList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessCreditNoteList] 
 GO
 CREATE TABLE [dbo].[BusinessCreditNoteList] ( 
 [Id]                 INT              IDENTITY(1,1)          NOT NULL,
 [DocumentNumber]     VARCHAR(20)                             NOT NULL,
 [Supplier]           VARCHAR(512)                            NOT NULL,
 [Customer]           VARCHAR(512)                            NOT NULL,
 [IssueDate]          DATETIME2                               NOT NULL,
 [InvoiceNumber]      VARCHAR(20)                                 NULL,
 [Storned]            BIT                                     NOT NULL,
 [TotalCurrencyId]    INT                                     NOT NULL,
 [Description]        TEXT                                        NULL,
 [TotalPriceWithVat]  NUMERIC(10,2)                           NOT NULL,
 [UserId]             INT                                     NOT NULL,
 [TimeStamp]          DATETIME2                               NOT NULL  CONSTRAINT [DF_CreditNoteList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_CreditNoteList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_CreditNoteList]  UNIQUE      NONCLUSTERED ([DocumentNumber] asc) ,
 CONSTRAINT [FK_CreditNoteList_CurrencyList] FOREIGN KEY ([TotalCurrencyId]) REFERENCES [dbo].[BasicCurrencyList] (Id) ,
 CONSTRAINT [FK_CreditNoteList_OutgoingInvoiceList] FOREIGN KEY ([InvoiceNumber]) REFERENCES [dbo].[BusinessOutgoingInvoiceList] (DocumentNumber) ,
 CONSTRAINT [FK_CreditNoteList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 