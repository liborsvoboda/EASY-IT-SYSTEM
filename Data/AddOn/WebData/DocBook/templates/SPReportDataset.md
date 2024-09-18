CREATE PROCEDURE [dbo].[ReportDataset]
@TableName varchar(50) = null,
@Sequence int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

-- Intended for use with param-ed reports.
-- To be called from various My-FyiReporting reports
--   - Various reports with their own layouts are called from VB app after setting Queue with usp_ReportQueue_Push()
--     each report then just contains : 

-- SET FMTONLY OFF;
-- EXEC usp_ReportQueue_Pop @TableName='SomeTablename', @Sequence='10'
-- 

DECLARE @ID int;
DECLARE @NAME varchar(50);
DECLARE @SQL nvarchar(MAX);
DECLARE @FILTER nvarchar(MAX);
DECLARE @SEARCH varchar(50);
DECLARE @SEARCHCOLUMNLIST nvarchar(MAX);
DECLARE @SEARCHFILTERIGNORE bit;
DECLARE @RECID varchar(50);
DECLARE @RECIDFILTERIGNORE bit;

DECLARE @SEPARATEDCOLUMNS nvarchar(MAX);


SELECT Top 1 
  @ID=[Id], 
  @NAME=[Name], 
  @SQL=[Sql], 
  @FILTER=[Filter], 
  @SEARCH=[Search], 
  @SEARCHCOLUMNLIST=[SearchColumnList],
  @SEARCHFILTERIGNORE=[SearchFilterIgnore],
  @RECID=[RecId],
  @RECIDFILTERIGNORE=[RecIdFilterIgnore]
FROM SystemReportQueueList WHERE [TableName]=@TableName AND [Sequence] = @Sequence; 

--PRERARE RECID
IF (@RECID = 0 OR (@RECIDFILTERIGNORE = 1 AND @FILTER <> '1=1')) BEGIN 
	SET @RECID = ''
END ELSE BEGIN
	SET @RECID = CONCAT(' AND Id=',@RECID);
END

--PRERARE SEARCH
IF (@SEARCH = '' OR (@SEARCHFILTERIGNORE = 1 AND @FILTER <> '1=1')) BEGIN
	SET @SEPARATEDCOLUMNS =  '1=1';
END ELSE BEGIN
	SELECT @SEPARATEDCOLUMNS = STRING_AGG (CONCAT(value,' LIKE ',char(39),'%',@SEARCH,'%',char(39)), ' OR ') FROM STRING_SPLIT (@SEARCHCOLUMNLIST, ',');  
END

	SET @SQL = CONCAT(@SQL,' WHERE 1=1 AND (', @FILTER,') AND (', @SEPARATEDCOLUMNS,') ',@RECID); 
	--PRINT @SQL; --FOR Debuging
	EXECUTE sp_executesql @SQL;
END