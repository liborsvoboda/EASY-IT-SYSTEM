




CREATE procedure [dbo].[SpTaskDB_SETRIGHTS]
AS
BEGIN 
	BEGIN TRY CREATE USER [easyitcenter] FOR LOGIN [easyitcenter] END TRY BEGIN CATCH END CATCH;
	BEGIN TRY ALTER ROLE [db_datareader] ADD MEMBER [easyitcenter]; END TRY BEGIN CATCH END CATCH;
	BEGIN TRY ALTER ROLE [db_datawriter] ADD MEMBER [easyitcenter]; END TRY BEGIN CATCH END CATCH;
	BEGIN TRY GRANT EXECUTE TO [easyitcenter]; END TRY BEGIN CATCH END CATCH;
END;

SELECT CONCAT('Read/Write/Exec Rights On Database EasyITCenter was set','') as 'MessageList';