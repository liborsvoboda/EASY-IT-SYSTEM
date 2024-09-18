




CREATE PROCEDURE [dbo].[SpAutoCleanEmailer]
AS

BEGIN
	DECLARE @AutoCleanEmailer int;
	SET NOCOUNT ON;

	--GET AutoCleanEmailer Configuration
	SELECT @AutoCleanEmailer = CAST(CAST(SUBSTRING(p.[Value],1,10) as varchar(10)) as int) FROM [dbo].[SystemParameterList] p WHERE p.[UserId] IS NULL AND p.[SystemName] = 'EmailerAutoCleanDayInterval';

	IF(@AutoCleanEmailer > 0) BEGIN
		DELETE FROM [dbo].[SolutionEmailerHistoryList] WHERE [TimeStamp] < DATEADD(DAY, -@AutoCleanEmailer, CAST(CURRENT_TIMESTAMP AS DATETIME));
	END
END
