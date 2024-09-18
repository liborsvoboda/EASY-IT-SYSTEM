


-- Its Procedure From Provider To Customer
-- @TargetLinkedDb  must be In Format: 95.183.52.33 or 95.183.52.33,1433 or SNRJDI\SLAMANAGEMENT

CREATE procedure [dbo].[SpProvider_ExportSqlScript](@TableName varchar(255)
)
AS
BEGIN 
	DECLARE @SqlScript as VARCHAR(MAX) ='';

	DECLARE @TableDefinitionList TABLE (RowNum int IDENTITY (1, 1) Primary key NOT NULL, [Template] text NULL);
	INSERT INTO @TableDefinitionList([Template]) EXEC sp_GetDDL @TableName;
	SELECT @SqlScript = [Template] FROM @TableDefinitionList WHERE RowNum = 1;

	SET @SqlScript = REPLACE(@SqlScript,'','''');
	SET @SqlScript = REPLACE(@SqlScript,' GO','');






END;


	SELECT @SqlScript as 'MessageList';
