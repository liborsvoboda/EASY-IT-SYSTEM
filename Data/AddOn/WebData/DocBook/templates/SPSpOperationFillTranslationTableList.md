



CREATE procedure [dbo].[SpOperationFillTranslationTableList]
AS
BEGIN 

DECLARE @tableList TABLE (RowID int not null identity(1,1) primary key,[TableName] nvarchar(250) NOT NULL);
DECLARE @columnList TABLE ([ColumnName] nvarchar(250) NOT NULL);
DECLARE @resultList TABLE ([SystemName] nvarchar(50) NOT NULL);

	--GET ALL TABLES
	INSERT INTO @tableList ([TableName])
	SELECT TABLE_NAME as 'TableList'
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_type = 'BASE TABLE'

	UNION 

	--GET ALL VIEWS
	SELECT 
	  TABLE_NAME as 'TableList'
	FROM INFORMATION_SCHEMA.VIEWS;

	--START Save For Result
	INSERT INTO @resultList ( [SystemName])
	SELECT LOWER(LEFT(T1.[TableName],1))+SUBSTRING(T1.[TableName],2,LEN(T1.[TableName])) FROM @tableList T1
	where NOT EXISTS (SELECT 1 FROM [dbo].[SystemTranslationList] T2 where T2.[SystemName] = T1.TableName);
	--END Save For Result

	INSERT INTO [dbo].[SystemTranslationList] ( [SystemName],[UserId])
	SELECT LOWER(LEFT(T1.[TableName],1))+SUBSTRING(T1.[TableName],2,LEN(T1.[TableName])),1 FROM @tableList T1
	where NOT EXISTS (SELECT 1 FROM [dbo].[SystemTranslationList] T2 where T2.[SystemName] = T1.TableName);

	declare @i int;declare @max int;
	select @i = min(RowID) from @tableList;
	select @max = max(RowID) from @tableList;
	DECLARE @columnName varchar(255);
	
	WHILE @i <= @max BEGIN

		SELECT @columnName = [TableName] FROM @tableList where RowID = @i;

		INSERT INTO @columnList ([ColumnName])
		SELECT COLUMN_NAME
		FROM INFORMATION_SCHEMA.COLUMNS T1
		WHERE TABLE_NAME = @columnName;

		SET @i = @i + 1
	END

	--START Save For Result
	INSERT INTO @resultList ( [SystemName])
	SELECT DISTINCT LOWER(LEFT(T1.[ColumnName],1))+SUBSTRING(T1.[ColumnName],2,LEN(T1.[ColumnName])) FROM @columnList T1
	WHERE NOT EXISTS (SELECT 1 FROM [dbo].[SystemTranslationList] T2 WHERE T2.[SystemName] = T1.[ColumnName]);
	--END Save For Result

	INSERT INTO [dbo].[SystemTranslationList] ( [SystemName],[UserId])
	SELECT DISTINCT LOWER(LEFT(T1.[ColumnName],1))+SUBSTRING(T1.[ColumnName],2,LEN(T1.[ColumnName])),1 FROM @columnList T1
	WHERE NOT EXISTS (SELECT 1 FROM [dbo].[SystemTranslationList] T2 WHERE T2.[SystemName] = T1.[ColumnName]);

	--Copy Translation from xxxListPage to xxxList
	UPDATE TargetTbl
	SET
		TargetTbl.DescriptionCz = SourceTbl.DescriptionCz,
		TargetTbl.DescriptionEn = SourceTbl.DescriptionEn
	FROM
		[dbo].[SystemTranslationList] AS TargetTbl
		INNER JOIN [dbo].[SystemTranslationList] AS SourceTbl
			ON SourceTbl.SystemName = CONCAT(TargetTbl.SystemName,'Page')
	WHERE
		LEN(TargetTbl.DescriptionCz) = 0 AND
		SourceTbl.SystemName LIKE '%ListPage'

	--Return List Of New Translation Words
	--SELECT * FROM @resultList;
	SELECT 'Update TranslationList From DB Schema is Completed' as 'MessageList';
END;
