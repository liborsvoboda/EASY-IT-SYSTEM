
 IF OBJECT_ID('[dbo].[SystemTranslationList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemTranslationList] 
 GO
 CREATE TABLE [dbo].[SystemTranslationList] ( 
 [Id]             INT              IDENTITY(1,1)          NOT NULL,
 [SystemName]     VARCHAR(50)                             NOT NULL,
 [DescriptionCz]  VARCHAR(500)                                NULL,
 [DescriptionEn]  VARCHAR(500)                                NULL,
 [UserId]         INT                                         NULL  CONSTRAINT [DF_SystemTranslationList_UserId] DEFAULT ((0)),
 [Timestamp]      DATETIME2                               NOT NULL  CONSTRAINT [DF_SystemTranslationList_Timestamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_SystemTranslationList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_SystemTranslationList]  UNIQUE      NONCLUSTERED ([SystemName] asc) ,
 CONSTRAINT [FK_SystemTranslationList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id) )
 
 
 GO
 
 

CREATE   TRIGGER [dbo].[TR_SystemTranslationList] ON [dbo].[SystemTranslationList]
FOR INSERT
AS
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @UserId int;DECLARE @RecId int;
	DECLARE @AutoFillDictionaries bit;
	DECLARE @SystemName varchar(50);DECLARE @DescriptionCz varchar(500);DECLARE @DescriptionEn varchar(500);
	SET @AutoFillDictionaries = 1;

	SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 0 FROM deleted)
		BEGIN -- INSERT
			
			SELECT @RecId = ins.Id from inserted ins;
			SELECT @UserId = ins.UserId from inserted ins;
			SELECT @SystemName = ins.SystemName from inserted ins;
			SELECT @DescriptionCz = ins.DescriptionCz from inserted ins;
			SELECT @DescriptionEn = ins.DescriptionEn from inserted ins;
			
			--GET AutoFilling Configuration
			SELECT @AutoFillDictionaries = CAST(CAST(SUBSTRING(p.[Value],1,10) as varchar(10)) as bit) FROM [dbo].[SystemParameterList] p WHERE p.[UserId] IS NULL AND p.[SystemName] = 'ServerTranslationAutoFillEnabled';

			IF (@AutoFillDictionaries = 1) BEGIN
				IF(@DescriptionCz IS NULL OR LEN(@DescriptionCz) = 0) BEGIN SET @DescriptionCz = @SystemName; END
				IF(@DescriptionEn IS NULL OR LEN(@DescriptionEn) = 0) BEGIN SET @DescriptionEn = @SystemName; END
				UPDATE dbo.SystemTranslationList SET [DescriptionCz] = @DescriptionCz, [DescriptionEn] = @DescriptionEn WHERE Id = @RecId;
			END
		END
END

 GO