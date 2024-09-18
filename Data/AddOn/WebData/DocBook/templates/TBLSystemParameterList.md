
 IF OBJECT_ID('[dbo].[SystemParameterList]') IS NOT NULL 
 DROP TABLE [dbo].[SystemParameterList] 
 GO
 CREATE TABLE [dbo].[SystemParameterList] ( 
 [Id]           INT              IDENTITY(1,1)          NOT NULL,
 [UserId]       INT                                         NULL,
 [SystemName]   VARCHAR(50)                             NOT NULL,
 [Value]        TEXT                                    NOT NULL,
 [Type]         VARCHAR(50)                             NOT NULL,
 [Description]  TEXT                                        NULL,
 [TimeStamp]    DATETIME2                               NOT NULL  CONSTRAINT [DF_ParameterList_TimeStamp] DEFAULT (getdate()),
 CONSTRAINT   [PK_ParameterList]  PRIMARY KEY CLUSTERED    ([Id] asc) ,
 CONSTRAINT   [IX_ParameterList]  UNIQUE      NONCLUSTERED ([SystemName] asc, [UserId] asc) ,
 CONSTRAINT [FK_ParameterList_UserList] FOREIGN KEY ([UserId]) REFERENCES [dbo].[SolutionUserList] (Id)  ON DELETE CASCADE )
 
 
 GO
 
 CREATE   TRIGGER [dbo].[TR_ParameterList] ON dbo.SystemParameterList
FOR INSERT
AS
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @UserId int;DECLARE @RecId int;DECLARE @SystemName varchar(50);	DECLARE @Value varchar(max);	
	DECLARE @Type varchar(20); DECLARE @Description varchar(MAX);

	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		
		SELECT @UserId = ins.UserId from inserted ins;
		
	END ELSE
		BEGIN -- INSERT

			SELECT @RecId = ins.[Id] from inserted ins;
			SELECT @UserId = ins.[UserId] from inserted ins;
			SELECT @SystemName = ins.[SystemName] from inserted ins;
			SELECT @Type  = ins.[Type] from inserted ins;

			SELECT @Value = CONVERT(varchar(MAX),p.[Value]), @Description = CONVERT(varchar(MAX),p.[Description]) from [dbo].[SystemParameterList] p WHERE p.Id =  @RecId ;
			
			IF (@UserId IS NULL) BEGIN
			
				INSERT INTO [dbo].[SystemParameterList]([UserId],[SystemName],[Value],[Type],[Description])
				SELECT DISTINCT pa.UserId, @SystemName, @Value, @Type, @Description
				FROM [dbo].[SystemParameterList] pa
				WHERE pa.UserId IS NOT NULL;
				
			END
		END
END /* ELSE 
BEGIN --DELETE
	SELECT @UserId = ins.[UserId] from inserted ins;
	SELECT @SystemName = ins.[SystemName] from inserted ins;
	
	IF (@UserId IS NULL) BEGIN
		DELETE FROM [dbo].[SystemParameterList] WHERE [SystemName] = @SystemName;
	END
END*/

 GO