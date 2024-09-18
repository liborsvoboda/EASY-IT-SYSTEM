
-- Procedure For Clone Database To NEW DATABASE With Create New User If username+Pw is Inserted
-- IF Usename OR password are null the User Is Cerated With Same name AND password as DATABASE



CREATE procedure [dbo].[DB_CLONE](@SourceDbName varchar(255), @NewDbName varchar(255),@Rewrite bit = false, @userName varchar(255) = NULL, @password varchar(255) = NULL)
AS
BEGIN 
	SET NOCOUNT ON;
	
	-- CREATE C:\Database Folder
	BEGIN TRY EXEC xp_cmdshell 'MD C:\Database'; END TRY
	BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber ,ERROR_MESSAGE() AS ErrorMessage; END CATCH;
	
	--CHECK AND SET USERNAME AND PASSWORD
	IF ( ISNULL(@userName,1) = 1 OR ISNULL(@password,1) = 1) BEGIN
		SET @userName = @NewDbName;
		SET @password = @NewDbName;
	END

	--DELETE DATABASE IF EXIST IN REWRITE MODE
	IF (@Rewrite = 1 AND DB_ID(@NewDbName) IS NOT NULL) BEGIN
		BEGIN TRY EXEC('DROP DATABASE ' + @NewDbName +';') END TRY BEGIN CATCH END CATCH;
	END


	--BACKUP SOURCE DATABASE
	DECLARE @backupfile as varchar(1024) = CONCAT('C:\Database\','_',@SourceDbName,'.bak');
	DECLARE @NewDBfileName as varchar(1024) = CONCAT('C:\Database\',@NewDbName,'.mdf');
	DECLARE @NewDBLogfileName as varchar(1024) = CONCAT('C:\Database\',@NewDbName,'_log','.ldf');
	DECLARE @OldDBLogName as varchar(1024) = CONCAT(@SourceDbName,'_log');
	DECLARE @NewDBLogName as varchar(1024) = CONCAT(@NewDbName,'_log');


	BEGIN TRY DBCC SHRINKFILE (2, 1) END TRY BEGIN CATCH END CATCH;
	BACKUP DATABASE @SourceDbName TO DISK = @backupfile;
	BEGIN TRY DBCC SHRINKFILE (2, 1) END TRY BEGIN CATCH END CATCH;
	BACKUP DATABASE @SourceDbName TO DISK = @backupfile;


	--CREATE NEW DFATABASE
	RESTORE DATABASE @NewDbName FROM DISK = @backupfile 
	WITH MOVE @SourceDbName TO @NewDBfileName,
	MOVE @OldDBLogName TO @NewDBLogfileName, 
	FILE = 2,RECOVERY,  REPLACE,  STATS = 10;
	EXEC('ALTER DATABASE ' + @NewDbName + ' SET MULTI_USER');

	--SET RIGHT LOGICAL FILE NAME
	BEGIN TRY EXEC('USE ' + @NewDbName +'; ALTER DATABASE ' + @NewDbName + ' MODIFY FILE (NAME= ' + @SourceDbName +' , NEWNAME= ' +@NewDbName +') ') END TRY BEGIN CATCH END CATCH;
	BEGIN TRY EXEC('USE ' + @NewDbName +'; ALTER DATABASE ' + @NewDbName + ' MODIFY FILE (NAME= ' + @OldDBLogName +' , NEWNAME= ' +@NewDBLogName +') ') END TRY BEGIN CATCH END CATCH;


	-- REMOVE Username For REINSERTING
	BEGIN TRY EXEC('USE ' + @NewDbName + '; DROP USER IF EXISTS '+ @userName) END TRY BEGIN CATCH END CATCH;

	--CREATE NEW USER IF NOT EXIST
	BEGIN TRY
	EXEC('USE ' + @NewDbName + '; CREATE LOGIN ' + @username + ' WITH PASSWORD = ''' + @password + ''', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF')
	--CREATE LOGIN @userName WITH PASSWORD = @password, DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
	END TRY 
	BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber ,ERROR_MESSAGE() AS ErrorMessage; END CATCH;

    --SET RIGHTS FOR USER
	BEGIN TRY EXEC('USE ' + @NewDbName + '; CREATE USER ' + @userName +' FOR LOGIN ' + @userName) END TRY BEGIN CATCH END CATCH;
	BEGIN TRY EXEC('USE ' + @NewDbName + '; ALTER ROLE [db_datareader] ADD MEMBER ' + @userName) END TRY BEGIN CATCH END CATCH;
	BEGIN TRY EXEC('USE ' + @NewDbName + '; ALTER ROLE [db_datawriter] ADD MEMBER ' + @userName) END TRY BEGIN CATCH END CATCH;
	BEGIN TRY EXEC('USE ' + @NewDbName + '; GRANT EXECUTE TO ' + @userName) END TRY BEGIN CATCH END CATCH;

END;
