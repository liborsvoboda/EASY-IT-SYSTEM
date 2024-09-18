

-- Its Procedure From Provider To Customer
-- @TargetLinkedDb  must be In Format: 95.183.52.33 or 95.183.52.33,1433 or SNRJDI\SLAMANAGEMENT

CREATE procedure [dbo].[SpProvider_SendSqlScript](@TargetIp varchar(255),@TargetDBName varchar(255),
@UserName varchar(255),@Password varchar(255),@TableName varchar(255)
)
AS
BEGIN 
	DECLARE @result as varchar(max);SET @result = 'Distribution SQLScript: ' + CHAR(13)+CHAR(10);
	DECLARE @stepNo as int = 0;DECLARE @SqlScript as VARCHAR(MAX) ='';

	

	SET @result +=CAST(@stepNo AS VARCHAR(10)) + ' Start Setting Linked Server' + CHAR(13)+CHAR(10);
	BEGIN TRY 
		BEGIN TRY EXEC sp_addlinkedserver @server = @TargetIp, @srvproduct=N'SQL Server'; END TRY BEGIN CATCH END CATCH;
		--EXEC sp_addlinkedserver @server = @TargetIp, @srvproduct=N'SQL Server', @provider=N'SQLNCLI';
		
		EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname = @TargetIp, @locallogin=N'easyitcenter', @useself=N'True', @rmtuser=NULL, @rmtpassword=NULL;
		EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname = @TargetIp, @locallogin = NULL , @useself = N'False', @rmtuser = N'sa', @rmtpassword = N'Hotel2023+'
		SET @result +=CAST(@stepNo AS VARCHAR(10))  + ' Setting Linked Server OK' + CHAR(13)+CHAR(10);
	END TRY
	BEGIN CATCH SELECT @result += CONCAT(CAST(@stepNo AS VARCHAR(10)) + ' Error No: ',ERROR_NUMBER(),' = Process Jumped',CHAR(13)+CHAR(10), ERROR_MESSAGE(),CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)); END CATCH;
	SET @stepNo += 10;





	SET @result += CAST(@stepNo AS VARCHAR(10)) + ' Start Preparing Script' + CHAR(13)+CHAR(10);
	BEGIN TRY 
		DECLARE @TableDefinitionList TABLE (RowNum int IDENTITY (1, 1) Primary key NOT NULL, [Template] text NULL);
		INSERT INTO @TableDefinitionList([Template]) EXEC sp_GetDDL @TableName;
		SELECT @SqlScript = [Template] FROM @TableDefinitionList WHERE RowNum = 1;

			SET @SqlScript = REPLACE(@SqlScript,'','''');
			SET @SqlScript = REPLACE(@SqlScript,' GO','');

		SET @result += CAST(@stepNo AS VARCHAR(10))  + '  Preparing Script OK' + CHAR(13)+CHAR(10);
	END TRY
	BEGIN CATCH SELECT @result += CONCAT(CAST(@stepNo AS VARCHAR(10)) + ' Error No: ',ERROR_NUMBER(),' = Process Jumped',CHAR(13)+CHAR(10), ERROR_MESSAGE(),CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)); END CATCH;
	SET @stepNo += 10;








	SET @result +=CAST(@stepNo AS VARCHAR(10)) + ' Start Executing Script' + CHAR(13)+CHAR(10);
	BEGIN TRY 
		DECLARE @TablePart AS VARCHAR(MAX) ='';
		DECLARE @TriggerPart AS VARCHAR(MAX) ='';

		PRINT @SqlScript;
		SELECT @TablePart = SUBSTRING(@SqlScript,0,CHARINDEX('TRIGGER',@SqlScript,0)), @TriggerPart = SUBSTRING(@SqlScript,CHARINDEX('TRIGGER',@SqlScript,0)+1,LEN(@SqlScript));
		EXEC (@TablePart);
		EXEC (@TriggerPart);

		SET @result +=CAST(@stepNo AS VARCHAR(10)) + ' Executing Script OK' + CHAR(13)+CHAR(10);
	END TRY
	BEGIN CATCH SELECT @result += CONCAT(CAST(@stepNo AS VARCHAR(10)) + ' Error No: ',ERROR_NUMBER(),' = Process Jumped',CHAR(13)+CHAR(10), ERROR_MESSAGE(),CHAR(13)+CHAR(10)+CHAR(13)+CHAR(10)); END CATCH;
	SET @stepNo += 10;



	SET @result +=CAST(@stepNo AS VARCHAR(10)) + ' Process Done' + CHAR(13)+CHAR(10);
	SELECT @result as 'MessageList';
END;


PRINT CONCAT('SQL Distribution Script Result: ', CHAR(13)+CHAR(10),@result);-- as 'MessageList';
