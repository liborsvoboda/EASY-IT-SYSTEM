
CREATE PROCEDURE [dbo].[SpImportTemplateDocFilesFromFolder] (
	 @FolderPath NVARCHAR (2048)
   )
AS
BEGIN
   DECLARE @tsql NVARCHAR (4000);DECLARE @Filename varchar(250);DECLARE @GroupId int;DECLARE @Sequence int;DECLARE @Description varchar(250);

   DECLARE @FilePathList TABLE (RowNum int IDENTITY (1, 1) Primary key NOT NULL,SubDirectory nvarchar(255), Depth smallint, FileFlag bit);
   DECLARE @RowCnt int; SET @RowCnt = 0;
   SET NOCOUNT ON

   SET @GroupId = 9;
   SET @Sequence = 1000;
   SET @Description = 'IMPORTED FILE';

   --CLEAN RECORDS WITH SAME DESCRIPTION
   --DELETE FROM [dbo].[DocSrvDocTemplateList] WHERE CONVERT(varchar(MAX),[Description]) IN ('IMPORTED FILE');

   INSERT INTO @FilePathList (SubDirectory, Depth, FileFlag) EXEC xp_dirtree @FolderPath, 0, 1;

   
   WHILE @RowCnt <= (SELECT COUNT([RowNum]) FROM @FilePathList)
	BEGIN
		SET @RowCnt = @RowCnt + 1;
		SELECT @Filename = SubDirectory FROM @FilePathList WHERE [RowNum] = @RowCnt;
		
		   SET @tsql = 'INSERT INTO DocSrvDocTemplateList ([GroupId],[Sequence],[Name],[Description],[UserId],[Template]) ' +
               ' SELECT 9,1000,' + '''' + @Filename + '''' + ',' + '''' + @Description + '''' + ',1, * ' + 
               'FROM Openrowset( Bulk ' + '''' + CONCAT(@FolderPath,'\',@Filename) + '''' + ', Single_Blob) as img';
		   EXEC (@tsql);
		   --PRINT (@tsql);
	END
	
   /* EXAMPLE FOR IMPORT ONLY ONE FILE
	  SET @tsql = 'INSERT INTO DocSrvDocTemplateList ([GroupId],[Sequence],[Name],[Description],[UserId], [Template]) ' +
               ' SELECT ' + '''' + @GroupId + '''' + ','+ '''' + @Sequence + '''' + ',' + '''' + @Filename + '''' + ',' + '''' + @Description + '''' + ',1, * ' + 
               'FROM Openrowset( Bulk ' + '''' + @TemplateFullPath + '''' + ', Single_Blob) as img'
      EXEC (@tsql)
   */

   SET NOCOUNT OFF
END

