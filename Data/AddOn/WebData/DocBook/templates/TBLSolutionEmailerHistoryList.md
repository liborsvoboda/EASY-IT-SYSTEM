
 IF OBJECT_ID('[dbo].[SolutionEmailerHistoryList]') IS NOT NULL 
 DROP TABLE [dbo].[SolutionEmailerHistoryList] 
 GO
 CREATE TABLE [dbo].[SolutionEmailerHistoryList] ( 
 [Id]         INT              IDENTITY(1,1)          NOT NULL,
 [Recipient]  VARCHAR(1024)                           NOT NULL,
 [Subject]    VARCHAR(1024)                           NOT NULL,
 [Email]      TEXT                                        NULL,
 [Status]     VARCHAR(2048)                           NOT NULL,
 [TimeStamp]  DATETIME2                               NOT NULL  CONSTRAINT [DF_SolutionEmailerHistoryList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SolutionEmailerHistoryList]  PRIMARY KEY CLUSTERED    ([Id] asc) )
 
 
 GO
 
 



CREATE   TRIGGER [dbo].[TR_SolutionEmailerHistoryList] ON [dbo].[SolutionEmailerHistoryList]
FOR INSERT
AS
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDATE
		SET NOCOUNT ON;
	END ELSE
		BEGIN -- INSERT
			EXEC [dbo].[SpAutoCleanEmailer];
		END
END

 GO