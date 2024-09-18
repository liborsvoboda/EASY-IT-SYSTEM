CREATE   TRIGGER [dbo].[TR_UserList] ON dbo.SolutionUserList
FOR INSERT
AS
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @UserId int;

	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE

		SELECT @UserId = ins.Id from inserted ins;

	END ELSE
		BEGIN -- INSERT

			SELECT @UserId = ins.Id from inserted ins;
			
			INSERT INTO [dbo].[SystemParameterList]([UserId],[SystemName],[Value],[Type],[Description])
			SELECT @UserId, pa.[SystemName],pa.[Value],pa.[Type],pa.[Description]
			FROM [dbo].[SystemParameterList] pa
			WHERE pa.UserId IS NULL;

		END
END /* ELSE 
BEGIN --DELETE
	SELECT @setActive = ins.[Active] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;
	SELECT @RecName = ins.[Name] from deleted ins;

	IF(@setActive = 1) BEGIN
		UPDATE [dbo].DocumentationList SET [Active] = 1 
		WHERE Id IN(SELECT TOP (1) MAX(d.Id) FROM [dbo].DocumentationList d WHERE d.Id <> @RecId AND d.[Name] = @RecName)
		;
	END
END
*/
