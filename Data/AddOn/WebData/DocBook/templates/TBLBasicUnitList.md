
 IF OBJECT_ID('[dbo].[BasicUnitList]') IS NOT NULL 
 DROP TABLE [dbo].[BasicUnitList] 
 GO
 CREATE TABLE [dbo].[BasicUnitList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(10)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_UnitList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_UnitList_TimeStamp] DEFAULT (getdate()),
 [Default]      BIT                                     NOT NULL  CONSTRAINT [DF_UnitList_Default] DEFAULT ((0)),
 CONSTRAINT   [PK_UnitList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_UnitList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_UnitList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 
CREATE   TRIGGER [dbo].[TR_UnitList] ON [dbo].[BasicUnitList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BasicUnitList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BasicUnitList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BasicUnitList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BasicUnitList WHERE Id <> @RecId)
		;
	END
END

 GO