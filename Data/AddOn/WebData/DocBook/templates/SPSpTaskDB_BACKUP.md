

CREATE procedure [dbo].[SpTaskDB_BACKUP]
AS
BEGIN 
	DECLARE @dbName as varchar(255) = DB_NAME();
	DECLARE @fileName as varchar(1024) = CONCAT('C:\Database\',@dbName,'_',FORMAT(GETDATE(),'yyyyMMdd_hhmmss'),'.bak');

	BEGIN TRY DROP USER [easyitcenter] END TRY BEGIN CATCH END CATCH;
	--DBCC SHRINKFILE (2, 1);
	BACKUP DATABASE @dbName TO DISK = @fileName;
	--DBCC SHRINKFILE (2, 1); 
	BACKUP DATABASE @dbName TO DISK = @fileName;

	BEGIN TRY CREATE USER [easyitcenter] FOR LOGIN [easyitcenter] END TRY BEGIN CATCH END CATCH;
	BEGIN TRY ALTER ROLE [db_datareader] ADD MEMBER [easyitcenter]; END TRY BEGIN CATCH END CATCH;
	BEGIN TRY ALTER ROLE [db_datawriter] ADD MEMBER [easyitcenter]; END TRY BEGIN CATCH END CATCH;
	BEGIN TRY GRANT EXECUTE TO [easyitcenter]; END TRY BEGIN CATCH END CATCH;
	
	
	--PATH ON LINUX:  '/var/backups/DBbackup/hotels.bak'
	SELECT CONCAT('Database Was Backuped to ','C:\Database\',@dbName,'_',FORMAT(GETDATE(),'yyyyMMdd_hhmmss'),'.bak') as 'MessageList';
END;
