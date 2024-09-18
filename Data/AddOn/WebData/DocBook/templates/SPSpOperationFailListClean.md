




CREATE procedure [dbo].[SpOperationFailListClean]
AS
BEGIN 
	TRUNCATE TABLE [dbo].[SolutionFailList];
	SELECT 'Solution Fails was Cleaned' as 'MessageList';
END;
