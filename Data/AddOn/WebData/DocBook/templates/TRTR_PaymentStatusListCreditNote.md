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
