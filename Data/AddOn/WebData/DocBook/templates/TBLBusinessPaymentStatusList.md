
 IF OBJECT_ID('[dbo].[BusinessPaymentStatusList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessPaymentStatusList] 
 GO
 CREATE TABLE [dbo].[BusinessPaymentStatusList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Default]      BIT                                     NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_PaymentStatusList_Active] DEFAULT ((1)),
 [Receipt]      BIT                                     NOT NULL  CONSTRAINT [DF_PaymentStatusList_Receipt] DEFAULT ((0)),
 [CreditNote]   BIT                                     NOT NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_PaymentStatusList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_PaymentStatusList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_PaymentStatusList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_PaymentStatusList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE   TRIGGER [dbo].[TR_PaymentStatusList] ON [dbo].[BusinessPaymentStatusList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BusinessPaymentStatusList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BusinessPaymentStatusList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BusinessPaymentStatusList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessPaymentStatusList WHERE Id <> @RecId)
		;
	END
END

 GO
 CREATE   TRIGGER [dbo].[TR_PaymentStatusListCreditNote] ON [dbo].[BusinessPaymentStatusList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setCreditNote bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setCreditNote = ins.[CreditNote] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setCreditNote = 1) BEGIN
			UPDATE [dbo].BusinessPaymentStatusList SET [CreditNote] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setCreditNote = ins.[CreditNote] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setCreditNote = 1) BEGIN
				UPDATE [dbo].BusinessPaymentStatusList SET [CreditNote] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setCreditNote = ins.[CreditNote] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setCreditNote = 1) BEGIN
		UPDATE [dbo].BusinessPaymentStatusList SET [CreditNote] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessPaymentStatusList WHERE Id <> @RecId)
		;
	END
END

 GO
 
CREATE   TRIGGER [dbo].[TR_PaymentStatusListReceipt] ON [dbo].[BusinessPaymentStatusList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setReceipt bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setReceipt = ins.[Receipt] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setReceipt = 1) BEGIN
			UPDATE [dbo].BusinessPaymentStatusList SET [Receipt] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setReceipt = ins.[Receipt] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setReceipt = 1) BEGIN
				UPDATE [dbo].BusinessPaymentStatusList SET [Receipt] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setReceipt = ins.[Receipt] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setReceipt = 1) BEGIN
		UPDATE [dbo].BusinessPaymentStatusList SET [Receipt] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessPaymentStatusList WHERE Id <> @RecId)
		;
	END
END

 GO