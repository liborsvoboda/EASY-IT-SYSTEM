
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
