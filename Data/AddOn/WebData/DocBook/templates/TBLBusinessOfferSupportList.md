
 IF OBJECT_ID('[dbo].[BusinessOfferSupportList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessOfferSupportList] 
 GO
 CREATE TABLE [dbo].[BusinessOfferSupportList] ( 
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
 [TimeStamp]          DATETIME2                               NOT NULL  CONSTRAINT [DF_OfferItemList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_OfferItemList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_OfferItemList_OfferList] FOREIGN KEY ([DocumentNumber]) REFERENCES [dbo].[BusinessOfferList] (DocumentNumber)  ON DELETE CASCADE ,
 CONSTRAINT [FK_OfferItemList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_OfferItemList] 
    ON [dbo].[BusinessOfferSupportList] ([DocumentNumber] asc)