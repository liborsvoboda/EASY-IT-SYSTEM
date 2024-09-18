
 IF OBJECT_ID('[dbo].[SystemReportList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemReportList] 
 GO
 CREATE TABLE [dbo].[SystemReportList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [PageName]     VARCHAR(50)                             NOT NULL,
 [SystemName]   VARCHAR(50)                             NOT NULL,
 [JoinedId]     BIT                                     NOT NULL  CONSTRAINT [DF_ReportList_JoinedId] DEFAULT ((0)),
 [Description]  TEXT                                        NULL,
 [ReportPath]   VARCHAR(500)                                NULL,
 [MimeType]     VARCHAR(100)                            NOT NULL,
 [File]         VARBINARY(max)                          NOT NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_ReportList_Active] DEFAULT ((1)),
 [Timestamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_ReportList_TimeStamp] DEFAULT (getdate()),
 [Default]      BIT                                     NOT NULL  CONSTRAINT [DF_ReportList_Default] DEFAULT ((0)),
 CONSTRAINT   [PK_ReportList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT [FK_ReportList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE   TRIGGER [dbo].[TR_ReportList] ON [dbo].[SystemReportList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN /* UPDADE */
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].SystemReportList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN /* INSERT */
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].SystemReportList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN /* DELETE */
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].SystemReportList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].SystemReportList WHERE Id <> @RecId)
		;
	END
END

 GO