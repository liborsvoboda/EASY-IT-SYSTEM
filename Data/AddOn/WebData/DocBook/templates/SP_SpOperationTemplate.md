

-- PROCEDURE TEMPLATE WITH LOGGING TO DATABASE FAILLIST
CREATE procedure [dbo].[SpOperationTemplate]
AS
BEGIN 
	DECLARE @Error VarChar(MAX);
	
	--DO PROCESS
	BEGIN TRY  
		
		SELECT 1;

		-- PROCESS COMPLETED
		SELECT 'Process Completed' as 'MessageList';
	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('ProcessIdentifier',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);

		-- PROCESS ERROR
		SELECT @Error as 'MessageList';
	END CATCH


END;
