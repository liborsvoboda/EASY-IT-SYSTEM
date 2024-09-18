
 IF OBJECT_ID('[dbo].[BusinessWarehouseList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessWarehouseList] 
 GO
 CREATE TABLE [dbo].[BusinessWarehouseList] ( 
 [Id]                   INT              IDENTITY(1,1)          NOT NULL,
 [Name]                 VARCHAR(50)                             NOT NULL,
 [Description]          TEXT                                        NULL,
 [UserId]               INT                                     NOT NULL,
 [AllowNegativeStatus]  BIT                                     NOT NULL,
 [Default]              BIT                                     NOT NULL,
 [LockedByStockTaking]  BIT                                     NOT NULL  CONSTRAINT [DF_WarehouseList_IsLocked] DEFAULT ((0)),
 [LastStockTaking]      DATETIME2                               NOT NULL  CONSTRAINT [DF_WarehouseList_LastStockTaking] DEFAULT (getdate()),
 [Active]               BIT                                     NOT NULL  CONSTRAINT [DF_WarehouseList_Active] DEFAULT ((1)),
 [TimeStamp]            DATETIME2                               NOT NULL  CONSTRAINT [DF_WarehouseList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WarehouseList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WarehouseList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_WarehouseList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE   TRIGGER [dbo].[TR_WarehouseList] ON [dbo].[BusinessWarehouseList]
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
			UPDATE [dbo].BusinessWarehouseList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BusinessWarehouseList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BusinessWarehouseList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessWarehouseList WHERE Id <> @RecId)
		;
	END
END

 GO