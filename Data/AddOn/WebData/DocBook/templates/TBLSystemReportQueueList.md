
 IF OBJECT_ID('[dbo].[SystemReportQueueList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemReportQueueList] 
 GO
 CREATE TABLE [dbo].[SystemReportQueueList] ( 
 [Id]                  INT              IDENTITY(1,1)          NOT NULL,
 [Name]                VARCHAR(50)                             NOT NULL,
 [Sequence]            INT                                     NOT NULL,
 [Sql]                 NVARCHAR(max)                           NOT NULL,
 [TableName]           VARCHAR(150)                            NOT NULL,
 [Filter]              NVARCHAR(max)                               NULL,
 [Search]              VARCHAR(50)                                 NULL,
 [SearchColumnList]    NVARCHAR(max)                               NULL,
 [SearchFilterIgnore]  BIT                                     NOT NULL  CONSTRAINT [DF_ReportQueue_SearchFilterIgnore] DEFAULT ((0)),
 [RecId]               INT                                         NULL,
 [RecIdFilterIgnore]   BIT                                     NOT NULL  CONSTRAINT [DF_ReportQueue_RecIdFilterIgnore] DEFAULT ((0)),
 [Timestamp]           DATETIME2                               NOT NULL  CONSTRAINT [DF_ReportQueueList_Timestamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ReportQueue]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ReportQueueList_1]  UNIQUE      NONCLUSTERED ([TableName] asc, [Sequence] asc) ,
 CONSTRAINT   [IX_ReportQueue]  UNIQUE      NONCLUSTERED ([Name] asc) )
 
 
 GO
 
 CREATE NONCLUSTERED INDEX [IX_ReportQueueList] 
    ON [dbo].[SystemReportQueueList] ([TableName] asc)