
 IF OBJECT_ID('[dbo].[BusinessBranchList]') IS NOT NULL 
 DROP TABLE [dbo].[BusinessBranchList] 
 GO
 CREATE TABLE [dbo].[BusinessBranchList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [CompanyName]  VARCHAR(150)                            NOT NULL,
 [ContactName]  VARCHAR(150)                                NULL,
 [Street]       VARCHAR(150)                            NOT NULL,
 [City]         VARCHAR(150)                            NOT NULL,
 [PostCode]     VARCHAR(20)                             NOT NULL,
 [Phone]        VARCHAR(20)                             NOT NULL,
 [Email]        VARCHAR(150)                                NULL,
 [BankAccount]  VARCHAR(150)                                NULL,
 [Ico]          VARCHAR(20)                                 NULL,
 [Dic]          VARCHAR(20)                                 NULL,
 [UserId]       INT                                     NOT NULL,
 [Active]       BIT                                     NOT NULL  CONSTRAINT [DF_BranchList_Active] DEFAULT ((1)),
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_BranchList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_BranchList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_BranchList]  UNIQUE      NONCLUSTERED ([CompanyName] asc) ,
 CONSTRAINT [FK_BranchList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 
CREATE   TRIGGER [dbo].[TR_BranchList] ON [dbo].[BusinessBranchList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setActive bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setActive = ins.[Active] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setActive = 1) BEGIN
			UPDATE [dbo].BusinessBranchList SET [Active] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setActive = ins.[Active] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setActive = 1) BEGIN
				UPDATE [dbo].BusinessBranchList SET [Active] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setActive = ins.[Active] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setActive = 1) BEGIN
		UPDATE [dbo].BusinessBranchList SET [Active] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessBranchList WHERE Id <> @RecId)
		;
	END
END

 GO
 GO
 
 EXEC sys.sp_addextendedproperty
          @name = N'MS_Description', @value = N'',
          @level0type = N'SCHEMA', @level0name = [dbo],
          @level1type = N'TABLE', @level1name = [BusinessBranchList];