



CREATE procedure [dbo].[SystemSpGetSystemPageList]
AS
BEGIN 

	--GET ALL TABLES
	SELECT CONCAT(TABLE_NAME, 'Page') as 'TableList'
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_type = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%SupportList%'

	UNION 

	--GET ALL VIEWS
	SELECT 
	  CONCAT(TABLE_NAME, 'Page') as 'TableList'
	FROM INFORMATION_SCHEMA.VIEWS
	WHERE TABLE_NAME NOT LIKE '%SupportList%'

	UNION 

	--GET ALL Custom Form Page Names

	SELECT [PageName] AS TABLE_NAME
	FROM [dbo].[SystemCustomPageList]
	WHERE Active = 1

END;
