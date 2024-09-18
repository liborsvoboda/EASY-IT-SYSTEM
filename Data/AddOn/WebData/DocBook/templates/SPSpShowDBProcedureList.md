





CREATE procedure [dbo].[SpShowDBProcedureList]
AS
BEGIN 
	SELECT SO.[type_desc], SO.[name], SM.[definition],SO.create_date,SO.modify_date 
	FROM sys.sql_modules SM INNER JOIN sys.Objects SO ON SM.Object_id = SO.Object_id 
	--WHERE SO.[type_desc] = 'SP'
	ORDER BY SO.[type_desc], SO.[name];
	;
END;

