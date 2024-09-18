



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
