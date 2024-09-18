
 IF OBJECT_ID('[dbo].[BusinessPaymentMethodList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessPaymentMethodList] 
 GO
 CREATE TABLE [dbo].[BusinessPaymentMethodList] ( 
 [Id]                    INT              IDENTITY(1,1)          NOT NULL,
 [Name]                  VARCHAR(20)                             NOT NULL,
 [Default]               BIT                                     NOT NULL,
 [Description]           TEXT                                        NULL,
 [AutoGenerateReceipt]   BIT                                     NOT NULL  CONSTRAINT [DF_PaymentMethodList_AutoGenerateReceipt] DEFAULT ((0)),
 [AllowGenerateReceipt]  BIT                                     NOT NULL  CONSTRAINT [DF_PaymentMethodList_AllowGenerateReceipt] DEFAULT ((0)),
 [UserId]                INT                                     NOT NULL,
 [Active]                BIT                                     NOT NULL  CONSTRAINT [DF_PaymentMethodList_Active] DEFAULT ((1)),
 [TimeStamp]             DATETIME2                               NOT NULL  CONSTRAINT [DF_PaymentMethodList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_PaymentMethodList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_PaymentMethodList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_PaymentMethodList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE   TRIGGER [dbo].[TR_PaymentMethodList] ON [dbo].[BusinessPaymentMethodList]
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
			UPDATE [dbo].BusinessPaymentMethodList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BusinessPaymentMethodList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BusinessPaymentMethodList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessPaymentMethodList WHERE Id <> @RecId)
		;
	END
END

 GO