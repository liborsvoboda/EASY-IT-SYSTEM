
 IF OBJECT_ID('[dbo].[BusinessMaturityList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessMaturityList] 
 GO
 CREATE TABLE [dbo].[BusinessMaturityList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [Name]         VARCHAR(50)                             NOT NULL,
 [Value]        INT                                     NOT NULL,
 [Default]      BIT                                     NOT NULL,
 [Description]  TEXT                                        NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_MaturityList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_MaturityList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_MaturityList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_MaturityList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT [FK_MaturityList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 
CREATE   TRIGGER [dbo].[TR_MaturityList] ON [dbo].[BusinessMaturityList]
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
			UPDATE [dbo].BusinessMaturityList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BusinessMaturityList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BusinessMaturityList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessMaturityList WHERE Id <> @RecId)
		;
	END
END

 GO