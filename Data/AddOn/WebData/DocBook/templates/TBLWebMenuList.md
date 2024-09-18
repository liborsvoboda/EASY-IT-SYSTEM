
 IF OBJECT_ID('[dbo].[WebMenuList]') IS NOT NULL 
 DROP TABLE [dbo].[WebMenuList] 
 GO
 CREATE TABLE [dbo].[WebMenuList] ( 
 [Id]             INT              IDENTITY(1,1)          NOT NULL,
 [GroupId]        INT                                     NOT NULL,
 [Sequence]       INT                                     NOT NULL,
 [Name]           VARCHAR(50)                             NOT NULL,
 [MenuClass]      VARCHAR(100)                                NULL,
 [Description]    VARCHAR(2096)                               NULL,
 [HtmlContent]    TEXT                                    NOT NULL,
 [UserMenu]       BIT                                     NOT NULL  CONSTRAINT [DF_WebMenuList_UserRestriction] DEFAULT ((0)),
 [AdminMenu]      BIT                                     NOT NULL  CONSTRAINT [DF_WebMenuList_AdminMenu] DEFAULT ((0)),
 [UserIPAddress]  VARCHAR(50)                                 NULL,
 [UserId]         INT                                     NOT NULL,
 [Active]         BIT                                     NOT NULL,
 [Default]        BIT                                     NOT NULL,
 [TimeStamp]      DATETIME2                               NOT NULL  CONSTRAINT [DF_WebMenuList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_WebMenuList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_WebMenuList]  UNIQUE      NONCLUSTERED ([Name] asc, [GroupId] asc) ,
 CONSTRAINT [FK_WebMenuList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) ,
 CONSTRAINT [FK_WebMenuList_WebGroupMenuList] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[WebGroupMenuList] (Id) )
 
 
 GO
 
 

CREATE   TRIGGER [dbo].[TR_WebMenuList] ON [dbo].[WebMenuList]
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
			UPDATE [dbo].WebMenuList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].WebMenuList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END/* ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].WebMenuList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].WebMenuList WHERE Id <> @RecId)
		;
	END
END*/

 GO