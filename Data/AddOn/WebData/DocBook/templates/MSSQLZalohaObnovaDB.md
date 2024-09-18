
--BACKUP
USE[SHOPINGER]
DROP USER [shopinger];
DBCC SHRINKFILE (SHOPINGER_log, 1);
BACKUP DATABASE [SHOPINGER] TO DISK = N'C:\Database\SHOPINGER.bak'
DBCC SHRINKFILE (SHOPINGER_log, 1);
BACKUP DATABASE [SHOPINGER] TO DISK = N'C:\Database\SHOPINGER.bak'
GO

USE [SHOPINGER]
BEGIN TRY CREATE USER [shopinger] FOR LOGIN [shopinger] END TRY BEGIN CATCH END CATCH;
BEGIN TRY ALTER ROLE [db_datareader] ADD MEMBER [shopinger]; END TRY BEGIN CATCH END CATCH;

-----------------------------------------------------------------------------------------------------

--RESTORE TO NEW FILES
ALTER DATABASE [SHOPINGER] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
RESTORE DATABASE [SHOPINGER] FROM DISK = N'C:\Database\SHOPINGER.bak' 
WITH MOVE N'SHOPINGER' TO N'C:\Database\SHOPINGER.mdf',  
     MOVE N'SHOPINGER_log' TO N'C:\Database\SHOPINGER_log.ldf', FILE = 2,
RECOVERY,  REPLACE,  STATS = 10;
ALTER DATABASE [SHOPINGER] SET MULTI_USER;

-----------------------------------------------------------------------------------------------------

--!!!!   AFTER UPLOAD DATABASE PLEASE START EASYBuilder Service or Icon from Desktop   !!!!
--!!!!                                                                                      !!!!
--!!!!                                                                                      !!!!
--!!!!               SET CORRECT CONNECTION STRING WITH NAME/PASSWORD: shopinger            !!!!
--!!!!                    IN  C:\ProgramData\EASYBuilder\config.json                   !!!!
--!!!!              (config example: Server=SQLSRV; or Server=192.168.1.35,1433;)           !!!!
--!!!!                                          AND                                         !!!!
--!!!!                 START THE EASYBuilder Service or Icon from Desktop              !!!!
--!!!!                                                                                      !!!!
--!!!!                                                                                      !!!!
--!!!!   AFTER UPLOAD DATABASE PLEASE START EASYBuilder Service or Icon from Desktop   !!!!

USE [master]
GO

--1] SET MANUALLY RIGHT CLICK/FACETS/SERVER SECURITY/XPCmdShellEnabled/TRUE
--2] SELECT AND EXECUTE SQL CODE
--3] MODIFY CONNECTION STRING IN CONFIG.JSON in folder c:\ProgramData\EASYBuilder\config.json
--E] config example: Server=SQLSRV; or Server=192.168.1.35,1433;
--4] START THE EASYBuilder Service or Icon from Desktop


-- CREATE C:\Database Folder
BEGIN TRY EXEC xp_cmdshell 'MD C:\Database'; END TRY
BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber ,ERROR_MESSAGE() AS ErrorMessage; END CATCH;
GO



-- UPLOAD DATABASE LICENSESRV
RESTORE DATABASE [LICENSESRV] FROM DISK = N'C:\Program Files (x86)\GroupWare-Solution.eu\EASYBuilder\MSSQL_DB\LICENSESRV.bak' 
WITH MOVE N'LICENSESRV' TO N'C:\Database\LICENSESRV.mdf',  
     MOVE N'LICENSESRV_log' TO N'C:\Database\LICENSESRV_log.ldf', FILE = 2, RECOVERY,  REPLACE,  STATS = 10;
ALTER DATABASE [LicenseSrv] SET MULTI_USER;
GO



-- CREATE USER 'LICENSESRV'
BEGIN TRY
	CREATE LOGIN [licensesrv] WITH PASSWORD=N'licensesrv', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
END TRY 
BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber ,ERROR_MESSAGE() AS ErrorMessage; END CATCH;
GO

USE [LICENSESRV]
BEGIN TRY CREATE USER [licensesrv] FOR LOGIN [licensesrv] END TRY BEGIN CATCH END CATCH;
BEGIN TRY ALTER ROLE [db_datareader] ADD MEMBER [licensesrv]; END TRY BEGIN CATCH END CATCH;
BEGIN TRY ALTER ROLE [db_datawriter] ADD MEMBER [licensesrv]; END TRY BEGIN CATCH END CATCH;
BEGIN TRY GRANT EXECUTE TO [licensesrv]; END TRY BEGIN CATCH END CATCH;


-----------------------------------------------------------------------------------------------------
sqlcmd -U username -P password -S .\SQLEXPRESS -d LICENSESRV -Q "EXEC DB_BACKUP"
sqlcmd -U username -P password -S .\SQLEXPRESS -d PRUVODKY -Q "EXEC DB_BACKUP"
sqlcmd -U username -P password -S .\SQLEXPRESS -d SHOPINGER -Q "EXEC DB_BACKUP"
sqlcmd -U username -P password -S .\SQLEXPRESS -d LICENSESHOPER -Q "EXEC DB_BACKUP"
sqlcmd -U username -P password -S .\SQLEXPRESS -d EASYBUILDER -Q "EXEC DB_BACKUP"
-----------------------------------------------------------------------------------------------------
sqlcmd -U username -P password -S .\SQLEXPRESS -Q "ALTER DATABASE [LICENSESRV] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;RESTORE DATABASE [LICENSESRV] FROM DISK = N'C:\Database\DEFAULT_DATABASES\LICENSESRV.bak' WITH MOVE N'LICENSESRV' TO N'C:\Database\LICENSESRV.mdf',  MOVE N'LICENSESRV_log' TO N'C:\Database\LICENSESRV_log.ldf', FILE = 2,RECOVERY,  REPLACE,  STATS = 10;ALTER DATABASE [LICENSESRV] SET MULTI_USER;"
sqlcmd -U username -P password -S .\SQLEXPRESS -d LICENSESRV -Q "EXEC DB_SETRIGHTS"

sqlcmd -U username -P password -S .\SQLEXPRESS -Q "ALTER DATABASE [PRUVODKY] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;RESTORE DATABASE [PRUVODKY] FROM DISK = N'C:\Database\DEFAULT_DATABASES\PRUVODKY.bak' WITH MOVE N'PRUVODKY' TO N'C:\Database\PRUVODKY.mdf',  MOVE N'PRUVODKY_log' TO N'C:\Database\PRUVODKY_log.ldf', FILE = 2,RECOVERY,  REPLACE,  STATS = 10;ALTER DATABASE [PRUVODKY] SET MULTI_USER;"
sqlcmd -U username -P password -S .\SQLEXPRESS -d PRUVODKY -Q "EXEC DB_SETRIGHTS"

sqlcmd -U username -P password -S .\SQLEXPRESS -Q "ALTER DATABASE [SHOPINGER] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;RESTORE DATABASE [SHOPINGER] FROM DISK = N'C:\Database\DEFAULT_DATABASES\SHOPINGER.bak' WITH MOVE N'SHOPINGER' TO N'C:\Database\SHOPINGER.mdf',  MOVE N'SHOPINGER_log' TO N'C:\Database\SHOPINGER_log.ldf', FILE = 2,RECOVERY,  REPLACE,  STATS = 10;ALTER DATABASE [SHOPINGER] SET MULTI_USER;"
sqlcmd -U username -P password -S .\SQLEXPRESS -d SHOPINGER -Q "EXEC DB_SETRIGHTS"

sqlcmd -U username -P password -S .\SQLEXPRESS -Q "ALTER DATABASE [LICENSESHOPER] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;RESTORE DATABASE [LICENSESHOPER] FROM DISK = N'C:\Database\DEFAULT_DATABASES\LICENSESHOPER.bak' WITH MOVE N'LICENSESHOPER' TO N'C:\Database\LICENSESHOPER.mdf',  MOVE N'LICENSESHOPER_log' TO N'C:\Database\LICENSESHOPER_log.ldf', FILE = 2,RECOVERY,  REPLACE,  STATS = 10;ALTER DATABASE [LICENSESHOPER] SET MULTI_USER;"
sqlcmd -U username -P password -S .\SQLEXPRESS -d LICENSESHOPER -Q "EXEC DB_SETRIGHTS"

sqlcmd -U username -P password -S .\SQLEXPRESS -Q "ALTER DATABASE [EASYBUILDER] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;RESTORE DATABASE [EASYBUILDER] FROM DISK = N'C:\Database\DEFAULT_DATABASES\EASYBUILDER.bak' WITH MOVE N'EASYBUILDER' TO N'C:\Database\EASYBUILDER.mdf',  MOVE N'EASYBUILDER_log' TO N'C:\Database\EASYBUILDER_log.ldf', FILE = 2,RECOVERY,  REPLACE,  STATS = 10;ALTER DATABASE [EASYBUILDER] SET MULTI_USER;"
sqlcmd -U username -P password -S .\SQLEXPRESS -d EASYBUILDER -Q "EXEC DB_SETRIGHTS"

-----------------------------------------------------------------------------------------------------
--!!!!      AFTER UPLOAD DATABASE PLEASE START SHOPINGERDATACenterServer Icon from Desktop  !!!!
--!!!!                                                                                      !!!!
--!!!!                                                                                      !!!!
--!!!!               SET CORRECT CONNECTION STRING WITH NAME/PASSWORD: shopinger            !!!!
--!!!!                    IN  C:\ProgramData\SHOPINGERDATACenter\config.json                !!!!
--!!!!              (config example: Server=SQLSRV; or Server=192.168.1.35,1433;)           !!!!
--!!!!                                          AND                                         !!!!
--!!!!                 START THE SHOPINGERDATACenterServer Icon from Desktop                !!!!
--!!!!                                                                                      !!!!
--!!!!                                                                                      !!!!
--!!!!      AFTER UPLOAD DATABASE PLEASE START SHOPINGERDATACenterServer Icon from Desktop  !!!!

USE [master]
GO

--1] SET MANUALLY RIGHT CLICK/FACETS/SERVER SECURITY/XPCmdShellEnabled/TRUE
--2] SELECT AND EXECUTE SQL CODE
--3] MODIFY CONNECTION STRING IN CONFIG.JSON in folder c:\ProgramData\SHOPINGERDATACenter\config.json
--E] config example: Server=SQLSRV; or Server=192.168.1.35,1433;
--4] START THE SHOPINGERDATACenterServer Icon from Desktop


-- CREATE C:\Database Folder
BEGIN TRY EXEC xp_cmdshell 'MD C:\Database'; END TRY
BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber ,ERROR_MESSAGE() AS ErrorMessage; END CATCH;
GO



-- UPLOAD DATABASE SHOPINGER
RESTORE DATABASE [SHOPINGER] FROM DISK = N'C:\Program Files (x86)\GroupWare-Solution.eu\SHOPINGERDATACenter\MSSQL_DB\SHOPINGER.bak' 
WITH MOVE N'SHOPINGER' TO N'C:\Database\SHOPINGER.mdf',  
     MOVE N'SHOPINGER_log' TO N'C:\Database\SHOPINGER_log.ldf', FILE = 2,
RECOVERY,  REPLACE,  STATS = 10;
ALTER DATABASE [SHOPINGER] SET MULTI_USER;
GO



-- CREATE USER 'shopinger'
BEGIN TRY
	CREATE LOGIN [shopinger] WITH PASSWORD=N'shopinger', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
END TRY 
BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber ,ERROR_MESSAGE() AS ErrorMessage; END CATCH;
GO

USE [SHOPINGER]
BEGIN TRY CREATE USER [shopinger] FOR LOGIN [shopinger] END TRY BEGIN CATCH END CATCH;
BEGIN TRY ALTER ROLE [db_datareader] ADD MEMBER [shopinger]; END TRY BEGIN CATCH END CATCH;
BEGIN TRY ALTER ROLE [db_datawriter] ADD MEMBER [shopinger]; END TRY BEGIN CATCH END CATCH;


--!!!!      AFTER UPLOAD DATABASE PLEASE START SHOPINGERDATACenterServer Icon from Desktop  !!!!
--!!!!                                                                                      !!!!
--!!!!                                                                                      !!!!
--!!!!               SET CORRECT CONNECTION STRING WITH NAME/PASSWORD: shopinger            !!!!
--!!!!                    IN  C:\ProgramData\SHOPINGERDATACenter\config.json                !!!!
--!!!!              (config example: Server=SQLSRV; or Server=192.168.1.35,1433;)           !!!!
--!!!!                                          AND                                         !!!!
--!!!!                 START THE SHOPINGERDATACenterServer Icon from Desktop                !!!!
--!!!!                                                                                      !!!!
--!!!!                                                                                      !!!!
--!!!!      AFTER UPLOAD DATABASE PLEASE START SHOPINGERDATACenterServer Icon from Desktop  !!!!





-----------------------------------------------------------------------------------------------------

