CREATE PROCEDURE [dbo].[SpImportSvgIcon] (
     @IconName NVARCHAR (100)
   , @ImageFullPath NVARCHAR (1000)
   )
AS
BEGIN
   DECLARE @tsql NVARCHAR (2000);
   SET NOCOUNT ON

   SET @tsql = 'INSERT INTO SystemSvgIconList ([Name],[UserId], [SvgIconPath]) ' +
               ' SELECT ' + '''' + @IconName + '''' + ',1, * ' + 
               'FROM Openrowset( Bulk ' + '''' + @ImageFullPath + '''' + ', Single_Blob) as img'
   EXEC (@tsql)
   SET NOCOUNT OFF
END
