
CREATE procedure [dbo].[SpOperationAutoUpdateDbTemplateDoc] --(@result varchar(255) OUTPUT) --string otput
AS
BEGIN 

	-- TYPES P=SQL_STORED_PROCEDURE, TR=TRIGGER, IF=FUNCTION, V=VIEW

	--CLEAN OLD DEFINITIONS
	DELETE FROM [dbo].[DocSrvDocTemplateList] WHERE CONVERT(varchar(MAX),[Description]) IN ('SQL_STORED_PROCEDURE','SQL_TRIGGER','VIEW','SQL_INLINE_TABLE_VALUED_FUNCTION','SQL_SCALAR_FUNCTION');
	DELETE FROM [dbo].[DocSrvDocTemplateList] WHERE CONVERT(varchar(MAX),[Description]) IN ('TABLE');

	--INSERT ALL OTHER DEFINITIONS
	INSERT INTO [dbo].[DocSrvDocTemplateList] ([GroupId],[InheritedCodeType],[Sequence],[Name],[Description],[Template],[UserId])
	SELECT 8,'MSSQL',2000, 
		CASE SO.[type] COLLATE DATABASE_DEFAULT
		 WHEN 'P' THEN CONCAT('SP ',SO.[name] COLLATE DATABASE_DEFAULT)
		 WHEN 'TR' THEN CONCAT('TR ',SO.[name] COLLATE DATABASE_DEFAULT)
		 WHEN 'IF' THEN CONCAT('FN ',SO.[name] COLLATE DATABASE_DEFAULT)
		 WHEN 'V' THEN CONCAT('VIEW ',SO.[name] COLLATE DATABASE_DEFAULT)
		 ELSE  CONCAT(SO.[type],SO.[name] COLLATE DATABASE_DEFAULT)
		 END AS [Name],
	SO.[type_desc] COLLATE DATABASE_DEFAULT, SM.[definition] COLLATE DATABASE_DEFAULT,1
	FROM sys.sql_modules SM INNER JOIN sys.Objects SO ON SM.Object_id = SO.Object_id ;


	-- INSERTING TABLE DEFINITIONS PART
	DECLARE @RowCnt int; SET @RowCnt = 0;
	DECLARE @TableName NVarChar(250);DECLARE @TableDefinition NVarChar(MAX);
	DECLARE @TableList TABLE (RowNum int IDENTITY (1, 1) Primary key NOT NULL,[TableName] nvarchar(250) NOT NULL);
	DECLARE @TableDefinitionList TABLE (RowNum int IDENTITY (1, 1) Primary key NOT NULL, [Template] text NULL);

	INSERT INTO @TableList ([TableName]) EXEC [dbo].SystemSpGetTableList;
	
	WHILE @RowCnt <= (SELECT COUNT([RowNum]) FROM @tableList)
	BEGIN
		SET @RowCnt = @RowCnt + 1;
		SELECT @TableName = [TableName] FROM @TableList WHERE [RowNum] = @RowCnt;

		INSERT INTO @TableDefinitionList([Template]) EXEC sp_GetDDL @TableName;
	END
	
	--INSERT TABLE DEFINITIONS
	INSERT INTO [dbo].[DocSrvDocTemplateList] ([GroupId],[InheritedCodeType],[Sequence],[Name],[Description],[Template],[UserId])
	SELECT 8,'MSSQL',1000,CONCAT('TBL ',tl.[TableName]),'TABLE',tdl.[Template],1 FROM @TableList tl LEFT JOIN @TableDefinitionList tdl ON tl.RowNum = tdl.RowNum;

	SELECT 'Update Templates in Doc Completed' as 'MessageList';
END;
