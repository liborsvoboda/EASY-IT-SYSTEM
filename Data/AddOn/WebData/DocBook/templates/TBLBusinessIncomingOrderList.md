
 IF OBJECT_ID('[dbo].[BusinessIncomingOrderList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessIncomingOrderList] 
 GO
 CREATE TABLE [dbo].[BusinessIncomingOrderList] ( 
 [Id]                   INT              IDENTITY(1,1)          NOT NULL,
 [DocumentNumber]       VARCHAR(20)                             NOT NULL,
 [Supplier]             VARCHAR(512)                            NOT NULL,
 [Customer]             VARCHAR(512)                            NOT NULL,
 [Storned]              BIT                                     NOT NULL,
 [TotalCurrencyId]      INT                                     NOT NULL,
 [Description]          TEXT                                        NULL,
 [CustomerOrderNumber]  VARCHAR(50)                                 NULL,
 [TotalPriceWithVat]    NUMERIC(10,2)                           NOT NULL,
 [UserId]               INT                                     NOT NULL,
 [TimeStamp]            DATETIME2                               NOT NULL  CONSTRAINT [DF_IncomingOrderList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_IncomingOrderList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_IncomingOrderList]  UNIQUE      NONCLUSTERED ([DocumentNumber] asc) ,
 CONSTRAINT [FK_IncomingOrderList_CurrencyList] FOREIGN KEY ([TotalCurrencyId]) REFERENCES [dbo].[BasicCurrencyList] (Id) ,
 CONSTRAINT [FK_IncomingOrderList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_IncomingOrderList_Supplier] 
    ON [dbo].[BusinessIncomingOrderList] ([Supplier] asc)