
 IF OBJECT_ID('[dbo].[BasicVatList]') IS NOT NULL 
 DROP TABLE [dbo].[BasicVatList] 
 GO
 CREATE TABLE [dbo].[BasicVatList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(20)                             NOT NULL,
 [Value]        NUMERIC(10,2)                           NOT NULL,
 [Default]      BIT                                     NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_VatList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_VatList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_VatList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_VatList]  UNIQUE      NONCLUSTERED ([Value] asc, [Active] asc) ,
 CONSTRAINT [FK_VatList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE   TRIGGER [dbo].[TR_VatList] ON [dbo].[BasicVatList]
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
			UPDATE [dbo].BasicVatList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BasicVatList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BasicVatList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BasicVatList WHERE Id <> @RecId)
		;
	END
END

 GO