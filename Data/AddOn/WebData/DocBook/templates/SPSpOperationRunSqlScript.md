
CREATE procedure [dbo].[SpOperationRunSqlScript](@sqlScript varchar(MAX))
AS
BEGIN 
	DECLARE @result VARCHAR(MAX) = '';

	BEGIN TRY 
		EXEC (@sqlScript);
	END TRY
	BEGIN CATCH SELECT @result = CONCAT('Error No: ',ERROR_NUMBER(),CHAR(13)+CHAR(10), ERROR_MESSAGE()); END CATCH;

	PRINT @result;
END;
