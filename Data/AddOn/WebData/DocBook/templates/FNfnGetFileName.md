Create FUNCTION fnGetFileName
(
    @fullpath nvarchar(1024)
) 
RETURNS nvarchar(1024)
AS
BEGIN
    IF(CHARINDEX('\', @fullpath) > 0)
       SELECT @fullpath = RIGHT(@fullpath, CHARINDEX('\', REVERSE(@fullpath)) -1)
       RETURN @fullpath
END