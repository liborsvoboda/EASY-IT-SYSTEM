
 IF OBJECT_ID('[dbo].[BusinessExchangeRateList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessExchangeRateList] 
 GO
 CREATE TABLE [dbo].[BusinessExchangeRateList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [CurrencyId]   INT                                     NOT NULL,
 [Value]        DECIMAL(10,2)                           NOT NULL,
 [ValidFrom]    DATE                                        NULL,
 [ValidTo]      DATE                                        NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_CourseList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_CourseList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_CourseList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_CourseList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) ,
 CONSTRAINT [FK_ExchangeRateList_CurrencyList] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[BasicCurrencyList] (Id) )
 
 