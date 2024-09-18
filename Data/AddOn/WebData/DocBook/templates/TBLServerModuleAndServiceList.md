
 IF OBJECT_ID('[dbo].[ServerModuleAndServiceList]') IS NOT NULL 
 DROP TABLE [dbo].[ServerModuleAndServiceList] 
 GO
 CREATE TABLE [dbo].[ServerModuleAndServiceList] ( 
 [Id]                     INT              IDENTITY(1,1)          NOT NULL,
 [InheritedPageType]      VARCHAR(50)                             NOT NULL,
 [Name]                   VARCHAR(50)                             NOT NULL,
 [InheritedLayoutType]    VARCHAR(50)                                 NULL  CONSTRAINT [DF_ServerModuleAndServiceList_InheritedLayoutType] DEFAULT ('MultiLangLayout'),
 [Description]            TEXT                                        NULL,
 [UrlSubPath]             VARCHAR(100)                                NULL,
 [OptionalConfiguration]  VARCHAR(2048)                               NULL,
 [AllowedRoles]           VARCHAR(500)                                NULL,
 [RestrictedAccess]       BIT                                     NOT NULL,
 [RedirectPathOnError]    VARCHAR(100)                                NULL,
 [CustomHtmlContent]      TEXT                                        NULL,
 [IsLoginModule]          BIT                                     NOT NULL  CONSTRAINT [DF_ServerModuleAndServiceList_IsLoginModule] DEFAULT ((0)),
 [PathSetAllowed]         BIT                                     NOT NULL,
 [RestrictionSetAllowed]  BIT                                     NOT NULL,
 [HtmlSetAllowed]         BIT                                     NOT NULL,
 [RedirectSetAllowed]     BIT                                     NOT NULL,
 [Active]                 BIT                                     NOT NULL  CONSTRAINT [DF_ServerModuleAndServiceList_Active] DEFAULT ((1)),
 [UserId]                 INT                                     NOT NULL,
 [TimeStamp]              DATETIME2                               NOT NULL  CONSTRAINT [DF_ServerModuleAndServiceList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ServerModuleAndServiceList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ServerModuleAndServiceList]  UNIQUE      NONCLUSTERED ([Name] asc) ,
 CONSTRAINT   [IX_ServerModuleAndServiceList_2]  UNIQUE      NONCLUSTERED ([UrlSubPath] asc) ,
 CONSTRAINT [FK_ServerModuleAndServiceList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 CREATE   TRIGGER [dbo].[TR_ServerModuleAndServiceList] ON [dbo].[ServerModuleAndServiceList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setIsLoginModule bit;DECLARE @RecId int;DECLARE @UrlSubPath VarChar(100);DECLARE @Type VarChar(50);DECLARE @ModulePathExist bit;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SET @ModulePathExist = 0;
		SELECT @setIsLoginModule = ins.[IsLoginModule] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;
		SELECT @UrlSubPath = ins.UrlSubPath from inserted ins;
		SELECT @Type = ins.[InheritedLayoutType] from inserted ins;

		--CheckExisting Only One Allowed HTML Module
		IF (@Type = 'FullHtmlPage' OR @Type = 'HtmlBodyOnly') BEGIN
			SELECT @ModulePathExist = 1 FROM [dbo].[ServerModuleAndServiceList] WHERE UrlSubPath = @UrlSubPath;
			IF (@ModulePathExist = 1) BEGIN
				RAISERROR('Can Be Only One Endpoint for Html Module', 16, 1)  
				ROLLBACK TRANSACTION
				RETURN
			END
		END

		--Changing Login Module Set
		IF(@setIsLoginModule = 1) BEGIN
			UPDATE [dbo].ServerModuleAndServiceList SET [IsLoginModule] = 0 WHERE Id <> @RecId; 
		END
	END ELSE
		BEGIN -- INSERT
			SET @ModulePathExist = 0;
			SELECT @setIsLoginModule = ins.[IsLoginModule] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;
			SELECT @UrlSubPath = ins.UrlSubPath from inserted ins;
			SELECT @Type = ins.[InheritedLayoutType] from inserted ins;

			--CheckExisting Only One Allowed HTML Module
			IF (@Type = 'FullHtmlPage' OR @Type = 'HtmlBodyOnly') BEGIN
				SELECT @ModulePathExist = 1 FROM [dbo].[ServerModuleAndServiceList] WHERE UrlSubPath = @UrlSubPath;
				IF (@ModulePathExist = 1) BEGIN
					RAISERROR('Can Be Only One Endpoint for Html Module', 16, 1)  
					ROLLBACK TRANSACTION
					RETURN
				END
			END

			--Changing Login Module Set
			IF(@setIsLoginModule = 1) BEGIN
				UPDATE [dbo].ServerModuleAndServiceList SET [IsLoginModule] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setIsLoginModule = ins.[IsLoginModule] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	--Changing Login Module Set
	IF(@setIsLoginModule = 1) BEGIN
		UPDATE [dbo].ServerModuleAndServiceList SET [IsLoginModule] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].ServerModuleAndServiceList WHERE Id <> @RecId)
		;
	END
END

 GO