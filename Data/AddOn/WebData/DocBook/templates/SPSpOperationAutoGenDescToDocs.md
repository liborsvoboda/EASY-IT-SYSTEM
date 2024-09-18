

CREATE procedure [dbo].[SpOperationAutoGenDescToDocs]
AS
BEGIN 
	DECLARE @Error VarChar(MAX);
	DECLARE @RazorPortal VarChar(MAX);
	DECLARE @PreparedNewsList VarChar(MAX); DECLARE @ProcessingTaskList VarChar(MAX); DECLARE @ChangeLogList VarChar(MAX);

	DECLARE @GroupId Int;DECLARE @Name VarChar(50);DECLARE @Description VarChar(Max);DECLARE @Template VarChar(Max);

	DECLARE @PrewTargetType VarChar(150);DECLARE @TargetType VarChar(150); DECLARE @StatusType VarChar(150);  DECLARE @Value VarChar(150); 
	DECLARE @Request VarChar(MAX); DECLARE @Documentation VarChar(MAX); DECLARE @Image VarBinary(Max);
	
	--Generate RazorPortalAutoDoc
	BEGIN TRY  
		DECLARE db_cursor1 CURSOR LOCAL FOR SELECT dtl.[GroupId],dtl.[Name],dtl.[Description],dtl.[Template] FROM [dbo].[DocSrvDocTemplateList] dtl WHERE dtl.Name LIKE '%cshtml%'
		
		SET @RazorPortal = char(13) + '# Úplný kód MVC Razor Portálu CSHTML,   ' + char(13) + char(13) + 's JS,Metro4, nutný Build,Vývoj 1 Měsíc, nyní se již nepoužívá   ' + char(13) + char(13) + char(13);
		OPEN db_cursor1;
		FETCH NEXT FROM db_cursor1 INTO @GroupId, @Name, @Description, @Template;
		
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			SET @RazorPortal += '### ' + @Name + '     
' + char(13) + char(13) + '
```http   
			' + char(13) + @Template + char(13) + '
```    
			' + char(13) + '
---   
			' + char(13)  + char(13);

			FETCH NEXT FROM db_cursor1 INTO @GroupId, @Name, @Description, @Template;
		END;
		CLOSE db_cursor1;
		DEALLOCATE db_cursor1;

		--SAVE RazorPortalAutoDoc
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(@GroupId,'Zrušený MVC Web Portal CsHtml','Je možné Vytěžit Generátory kódu a Objekty Metro4',100,@RazorPortal,@RazorPortal,1,1,1);

	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('RazorPortalAutoDoc',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH;


	--Generate XamlAutoDoc
	BEGIN TRY  
		DECLARE db_cursor2 CURSOR LOCAL FOR SELECT dtl.[GroupId],dtl.[Name],dtl.[Description],dtl.[Template] FROM [dbo].[DocSrvDocTemplateList] dtl WHERE dtl.Name LIKE '%XAML%'
		
		SET @RazorPortal = char(13) + '# ESB -WPF XAML Šablony,   
' + char(13) + char(13) + '
Standardní I mediální šablony pro Vývoj Systému   ' + char(13) + char(13) + char(13);
		OPEN db_cursor2;
		FETCH NEXT FROM db_cursor2 INTO @GroupId, @Name, @Description, @Template;
		
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			SET @RazorPortal += '### ' + @Name + '     
			' + char(13) + char(13) + '
```xml   
			' + char(13) + @Template + char(13) + '
```    
			' + char(13) + '
---   
			' + char(13)  + char(13);

			FETCH NEXT FROM db_cursor2 INTO @GroupId, @Name, @Description, @Template;
		END;
		CLOSE db_cursor2;
		DEALLOCATE db_cursor2;

		--SAVE XAMLTemplatesAutoDoc
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(@GroupId,'XAML Šablony Systému','Těchto pár Šablon Stačí na tvorbu Multimediálního Systému',300,@RazorPortal,@RazorPortal,1,1,1);

	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('XAMLTemplatesAutoDoc',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH;

	--Generate C#APIAutoDoc
	BEGIN TRY  
		DECLARE db_cursor3 CURSOR LOCAL FOR SELECT dtl.[GroupId],dtl.[Name],dtl.[Description],dtl.[Template] FROM [dbo].[DocSrvDocTemplateList] dtl WHERE dtl.Name LIKE '%C# API%'
		
		SET @RazorPortal = char(13) + '# EIC- C# API Šablony,   
' + char(13) + char(13) + 'Standardní C# API pro Vývoj Babkend APi Služeb   
' + char(13) + char(13) + char(13);
		OPEN db_cursor3;
		FETCH NEXT FROM db_cursor3 INTO @GroupId, @Name, @Description, @Template;
		
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			SET @RazorPortal += '### ' + @Name + '     
' + char(13) + char(13) + '
```csharp   
			' + char(13) + @Template + char(13) + '
```    
			' + char(13) + '
---   
			' + char(13)  + char(13);

			FETCH NEXT FROM db_cursor3 INTO @GroupId, @Name, @Description, @Template;
		END;
		CLOSE db_cursor3;
		DEALLOCATE db_cursor3;

		--SAVE C#APITemplatesAutoDoc
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(@GroupId,'API Šablony Systému','Těchto pár Šablon Stačí na tvorbu Libovolného Backend Serveru',300,@RazorPortal,@RazorPortal,1,1,1);

	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('APITemplatesAutoDoc',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH;

	--Generate DBTablesAutoDoc
	BEGIN TRY  
		DECLARE db_cursor4 CURSOR LOCAL FOR SELECT dtl.[GroupId],dtl.[Name],dtl.[Description],dtl.[Template] FROM [dbo].[DocSrvDocTemplateList] dtl WHERE dtl.[Description] LIKE 'TABLE';
		
		SET @RazorPortal = char(13) + '# DB - Export Tabulek Řešení,   
' + char(13) + char(13) + 'Zde najdete Všechny Tabulky Řešení, Struktura má Centrální Logiku, která zamezuje vzniku chyb    
' + char(13) + char(13) + char(13);
		OPEN db_cursor4;
		FETCH NEXT FROM db_cursor4 INTO @GroupId, @Name, @Description, @Template;
		
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			SET @RazorPortal += '### ' + @Name + '     
' + char(13) + '
```sql   
			' + char(13) + @Template + char(13) + '
```    
			' + char(13) + '
---   
			' + char(13)  + char(13);

			FETCH NEXT FROM db_cursor4 INTO @GroupId, @Name, @Description, @Template;
		END;
		CLOSE db_cursor4;
		DEALLOCATE db_cursor4;

		--SAVE DBTablesAutoDoc
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(@GroupId,'DB Tabulky Řešení','Struktura Tabulek te stále Téměř totožná, Pár Pravidel stačí k vytvoření dokonalého Systému',100,@RazorPortal,@RazorPortal,1,1,1);

	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('DBTablesAutoDoc',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH;

	--Generate DBProceduresAutoDoc
	BEGIN TRY  
		DECLARE db_cursor5 CURSOR LOCAL FOR SELECT dtl.[GroupId],dtl.[Name],dtl.[Description],dtl.[Template] FROM [dbo].[DocSrvDocTemplateList] dtl WHERE dtl.[Description] LIKE 'SQL_STORED_PROCEDURE';
		
		SET @RazorPortal = char(13) + '# DB - Export Procedur Řešení,   
' + char(13) + char(13) + 'Zde procedury pro automatizaci, Hromadné Operace, Správu DB    
' + char(13) + char(13) + char(13);
		OPEN db_cursor5;
		FETCH NEXT FROM db_cursor5 INTO @GroupId, @Name, @Description, @Template;
		
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			SET @RazorPortal += '### ' + @Name + '     
' + char(13) + '
```sql   
			' + char(13) + @Template + char(13) + '
```    
			' + char(13) + '
---   
			' + char(13)  + char(13);

			FETCH NEXT FROM db_cursor5 INTO @GroupId, @Name, @Description, @Template;
		END;
		CLOSE db_cursor5;
		DEALLOCATE db_cursor5;

		--SAVE DBProceduresAutoDoc
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(@GroupId,'DB Procedury Řešení','Vytvořené procedury pro automatizaci, Hromadné Operace, Správu DB',200,@RazorPortal,@RazorPortal,1,1,1);

	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('DBProceduresAutoDoc',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH;


	
	--Generate DBTriggersAutoDoc
	BEGIN TRY  
		DECLARE db_cursor6 CURSOR LOCAL FOR SELECT dtl.[GroupId],dtl.[Name],dtl.[Description],dtl.[Template] FROM [dbo].[DocSrvDocTemplateList] dtl WHERE dtl.[Description] LIKE 'SQL_TRIGGER';
		
		SET @RazorPortal = char(13) + '# DB - Export Procedur Řešení,   
' + char(13) + 'Zde najdete procedury, které stanovují pevnou a jasnou strukturu a vazby již v databázi   
' + char(13) + char(13) + char(13);
		OPEN db_cursor6;
		FETCH NEXT FROM db_cursor6 INTO @GroupId, @Name, @Description, @Template;
		
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			SET @RazorPortal += '### ' + @Name + '     
			' + char(13) + char(13) + '
```sql   
			' + char(13) + @Template + char(13) + '
```    
			' + char(13) + '
---   
			' + char(13);

			FETCH NEXT FROM db_cursor6 INTO @GroupId, @Name, @Description, @Template;
		END;
		CLOSE db_cursor6;
		DEALLOCATE db_cursor6;

		--SAVE DBTriggersAutoDoc
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(@GroupId,'DB Triggery','Základem je nastavit pevnou a jasnou strukturu a vazby již v databázi',300,@RazorPortal,@RazorPortal,1,1,1);

	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('DBTriggersAutoDoc',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH;

	--Generate DBViewAutoDoc
	BEGIN TRY  
		DECLARE db_cursor7 CURSOR LOCAL FOR SELECT dtl.[GroupId],dtl.[Name],dtl.[Description],dtl.[Template] FROM [dbo].[DocSrvDocTemplateList] dtl WHERE dtl.[Description] LIKE 'VIEW' 
		OR dtl.[Description] LIKE 'SQL_SCALAR_FUNCTION' OR dtl.[Description] LIKE 'SQL_INLINE_TABLE_VALUED_FUNCTION';
		
		SET @RazorPortal = char(13) + '# DB - Export Views a Funkcí Řešení,   
' + char(13) + 'Zde najdete složené Datapohledy a Funkce   
' + char(13) ;
		OPEN db_cursor7;
		FETCH NEXT FROM db_cursor7 INTO @GroupId, @Name, @Description, @Template;
		
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			SET @RazorPortal += '### ' + @Name + '     
			' + char(13) + '
```sql   ' + char(13) + @Template  + char(13) + '
```    ' + char(13) + '
---   
			' + char(13)  + char(13);

			FETCH NEXT FROM db_cursor7 INTO @GroupId, @Name, @Description, @Template;
		END;
		CLOSE db_cursor7;
		DEALLOCATE db_cursor7;

		--SAVE DBViewsAutoDoc
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(@GroupId,'DB View a Funkce','Složené Datapohledy a Funkce',400,@RazorPortal,@RazorPortal,1,1,1);

	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('DBViewsAutoDoc',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH;


	--END OF EXPORT TEMPLATES

	--START EXPORT PREPARED NEWS / CHANGELOG 

	--Generate Prepared News, Processing, ChangeLog
	BEGIN TRY  
		DECLARE db_cursor8 CURSOR LOCAL FOR SELECT stl.[InheritedTargetType],stl2.DescriptionCz,stl.[Message], stl.[Documentation], stl.[Image]
		FROM [dbo].[SolutionTaskList] stl,  [dbo].[SystemTranslationList] stl2
		WHERE stl.[InheritedStatusType]  = stl2.SystemName AND stl.[InheritedStatusType] = 'Waiting'
		ORDER BY stl.[InheritedTargetType];
		--	CASE stl.[InheritedStatusType] 
		--	WHEN 'Done' THEN 1
		--	WHEN 'Upgrading' THEN 2
		--	WHEN 'Processing' THEN 2	
		--	WHEN 'Waiting' THEN 3
		--	ELSE 4
		--	END ASC,
		
		--	stl.[TimeStamp];
	

		SET @PreparedNewsList = char(13) + '# Připravované Novinky,   
		' + char(13) + char(13) + 'Zde je seznam dalších chystaných funkcionalit, které posouvá vývoj.   
		To Hlavní Je přípravaWeb Portálu ONLINE s Vývojem kódu Mnanagovaným Centrálním Řešením.   
		to Nabízí Vždy aktializace po ruce Obosměrné rozšiřování Projektů a Centralizaci Agend.   
		Tím z tohoto řešení Bude Nejlevnější IT šeření Tak Rozsáhlého Charakteru.    
		' + char(13) + char(13) + char(13);

		OPEN db_cursor8  
		FETCH NEXT FROM db_cursor8 INTO @TargetType, @StatusType, @Request, @Documentation ,@Image
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
			IF(@PrewTargetType <> @TargetType) BEGIN
			SET @PreparedNewsList += '
---    ' + char(13) + '### ' + @TargetType + '     
			' + char(13);
			END
			
			SET @PreparedNewsList +='1. ' + @Request + char(13);
			
			SET @PrewTargetType = @TargetType;
			FETCH NEXT FROM db_cursor8 INTO @TargetType, @StatusType, @Request, @Documentation ,@Image
		END;
		CLOSE db_cursor8;
		DEALLOCATE db_cursor8;


		--SAVE Prepared News
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(7,'Připravované Novinky','Seznam nejprioritnějších Rozšíření Systému. Ty budou zaimlemetovány během 6 měsíců
		protože se tomu můžu věnovat jen ve volném čase. Nejen Itéčkáři v naší zemi se místo vzdělávání soustředí jak pěníze čerpat
		místo tvořit přidanými hodnotami své činnosti. Připraveno je jíž přes více jak 500 vylepšení. Stačí stahovat z Github.
		',30,@PreparedNewsList,@PreparedNewsList,1,1,1);


	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('PreparedNews',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH


		--Generate Processing, ChangeLog
	BEGIN TRY  
		DECLARE db_cursor9 CURSOR LOCAL FOR SELECT stl.[InheritedTargetType],stl2.DescriptionCz,stl.[Message], stl.[Documentation], stl.[Image]
		FROM [dbo].[SolutionTaskList] stl,  [dbo].[SystemTranslationList] stl2
		WHERE stl.[InheritedStatusType]  = stl2.SystemName AND stl.[InheritedStatusType] <> 'Waiting'
		ORDER BY stl.[InheritedStatusType], stl.[InheritedTargetType];


		SET @PreparedNewsList = char(13) + '# Zpracovávané a zpracované Funkcionality,   
		' + char(13) + char(13) + 'Zde je vypsána ani setina toho co vše Dané projekty Umí.    
Myslím že chystaný vývoj Online z Webu a Editorů mluví za vše. Tak neplýtvejte financemi na    
čas zaměstanců či jiná řešení A Vyzkoušejte 30 denní verzu zdarma.     
		' + char(13) + char(13) + char(13);

		OPEN db_cursor9  
		FETCH NEXT FROM db_cursor9 INTO @TargetType, @StatusType, @Request, @Documentation ,@Image
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
			IF(@PrewTargetType <> @TargetType) BEGIN
				SET @PreparedNewsList += char(13) + '### ' + @TargetType + '     
' + char(13);
			END
			
			SET @PreparedNewsList +='**' + @StatusType + '**
' + '1. ' + @Request + char(13) + '- ' + @Documentation  + char(13) + char(13) + '
---   
' + char(13);
			
			SET @PrewTargetType = @TargetType;
			FETCH NEXT FROM db_cursor9 INTO @TargetType, @StatusType, @Request, @Documentation ,@Image
		END;
		CLOSE db_cursor9;
		DEALLOCATE db_cursor9;


		--SAVE Changelog
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(7,'Zpracováváné A ChangeLog','Changelog jsem začal prat teprve ted. Každý úsek který vidíte, je výsledkem 2dnů práce macimálně.    
Pro představu co vše se dá zvládnout. Vyzkoušejte nebo požádejte o instalaci u vás ve firměš či doma a zkuste     
vyzkoušet vo vše dané řešení nabízí. ',800,@PreparedNewsList,@PreparedNewsList,1,1,1);


	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('Changelog',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH


	
	--GenerateServerConfig
	BEGIN TRY  
		DECLARE db_cursor11 CURSOR LOCAL FOR SELECT sl.InheritedGroupName,sl.[Key],sl.[Description]
		FROM [dbo].[ServerSettingList] sl
		ORDER BY sl.InheritedGroupName;


		SET @PreparedNewsList = char(13) + '# Základní Konfigurace Serveru,   
		' + char(13) + char(13) + 'Zde je Aktuální základní konfigurace serveru.Jsou do ní vytažené jen ty nejstěžejnější   
nastavení, protože k dispozici sich je stonásobně víckrát. 
To Vyplývá z počtu přes 100 nasazených technologií v 1 řešení parfekně vyladěných pro dokonalou práci.
' + char(13);

		OPEN db_cursor11  
		FETCH NEXT FROM db_cursor11 INTO @TargetType, @StatusType, @Documentation
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
			IF(@PrewTargetType <> @TargetType) BEGIN
				SET @PreparedNewsList += + char(13) + '
---   
' + char(13) + '# ' + @TargetType + '         
' + char(13);
			END
			
			SET @PreparedNewsList +='**' + @StatusType + '**
' + char(13) + '- ' + @Documentation  + char(13) + char(13);
			
			SET @PrewTargetType = @TargetType;
			FETCH NEXT FROM db_cursor11 INTO @TargetType, @StatusType,@Documentation
		END;
		CLOSE db_cursor11;
		DEALLOCATE db_cursor11;


		--SAVE GenerateServerConfig
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(2,'Základní Konfigurace Serveru','Zde je Výčet Konfigurace serveru a popis co dané volby Dělají a znamenají. 
Když si je namalujete do pavouka zjistíte že to výkrývá téměř celé IT.',30,@PreparedNewsList,@PreparedNewsList,1,1,1);


	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('GenerateServerConfig',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH


		
	--GenerateAgendasList
	BEGIN TRY  
		DECLARE db_cursor12 CURSOR LOCAL FOR SELECT sg.SystemName,sm.MenuType,sm.FormPageName, sm.[Description]
		FROM [dbo].[SystemMenuList] sm, [dbo].[SystemGroupMenuList] sg
		WHERE sg.Id = sm.GroupId
		ORDER BY sg.SystemName, CASE sm.MenuType
			WHEN 'Dial' THEN 1
			WHEN 'Agenda' THEN 2
			ELSE 3
			END;


		SET @PreparedNewsList = char(13) + '# Seznam Implementovaných Agend,   
		' + char(13) + char(13) + 'Zde je Seznam implementovaných Agend Systému. Systém je aktuálně zaměřen na Správu IT, a všeho Okolo.
Je Modifikován tak Aby zvládnul všechny možné typy požadavků. Upravit sio je pro Svá data je otázkou v řádech týdnů, protože
disponuje všemi možnými nástroji pro zpracování každého typu požadavku.
' + char(13);

		OPEN db_cursor12  
		FETCH NEXT FROM db_cursor12 INTO @TargetType, @StatusType,@Value, @Documentation
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
			IF(@PrewTargetType <> @TargetType) BEGIN
				SET @PreparedNewsList += + char(13) + '
---   
' + char(13) + '# ' + @TargetType + '         
' + char(13);
			END
			
			SET @PreparedNewsList +='- ' + @StatusType + '  **' + @Value + '**
' + char(13) + '*' + @Documentation  + '*' + char(13) + '
```    ' + char(13);
			
			SET @PrewTargetType = @TargetType;
			FETCH NEXT FROM db_cursor12 INTO @TargetType, @StatusType,@Value, @Documentation
		END;
		CLOSE db_cursor12;
		DEALLOCATE db_cursor12;


		--SAVE GenerateAgendasList
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(1,' Seznam Implementovaných Agend','Zde je Seznam implementovaných Agend Systému. Systém je aktuálně zaměřen na Správu IT, a všeho Okolo.
Je Modifikován tak Aby zvládnul všechny možné typy požadavků. Upravit sio je pro Svá data je otázkou v řádech týdnů, protože
disponuje všemi možnými nástroji pro zpracování každého požadavku.',30,@PreparedNewsList,@PreparedNewsList,1,1,1);


	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('GenerateAgendasList',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH



		--GenerateSystemModules
	BEGIN TRY  
		DECLARE db_cursor13 CURSOR LOCAL FOR SELECT sm.ModuleType, sm.[Name], sm.[Description]
		FROM [dbo].[SystemModuleList] sm
		ORDER BY sm.ModuleType;


		SET @PreparedNewsList = char(13) + '# Seznam ukázkových Modulů Systému,   
		' + char(13) + char(13) + 'Do Systému lze připojit libovolný Software, Libovolná Web Aplikace, či Libovolné Rozšíření,Framework atd.
To z něho Dělá naprostý Unikát. Webové Aplikace se tváří jako část Systému, Aplikace se ovírají Externě ale máte vše po ruce.
a Github nabízí Tisíce hotových řešení. Nestačí že Systém sám je už Systém Generátor a generuje Nové Agendy Skoro sám, ještě lze
připojit naprosto cokoliv. Systém má totiž v sobě na pozadí vlastní Web Server.
' + char(13);

		OPEN db_cursor13  
		FETCH NEXT FROM db_cursor13 INTO @TargetType, @StatusType, @Documentation
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
			IF(@PrewTargetType <> @TargetType) BEGIN
				SET @PreparedNewsList += + char(13) + '
---   
' + char(13) + '# ' + @TargetType + '         
' + char(13);
			END
			
			SET @PreparedNewsList +='  **' + @StatusType + '**
' + char(13) + '*' + @Documentation  + '*' + char(13) + '
' + char(13);
			
			SET @PrewTargetType = @TargetType;
			FETCH NEXT FROM db_cursor13 INTO @TargetType, @StatusType, @Documentation
		END;
		CLOSE db_cursor13;
		DEALLOCATE db_cursor13;


		--SAVE GenerateSystemModules
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(1,'Seznam ukázkových Modulů Systému','Do Systému lze připojit libovolný Software, Libovolná Web Aplikace, či Libovolné Rozšíření,Framework atd.
To z něho Dělá naprostý Unikát. Webové Aplikace se tváří jako část Systému, Aplikace se ovírají Externě ale máte vše po ruce.
a Github nabízí Tisíce hotových řešení. Nestačí že Systém sám je už Systém Generátor a generuje Nové Agendy Skoro sám, ještě lze
připojit naprosto cokoliv. Systém má totiž v sobě na pozadí vlastní Web Server.',40,@PreparedNewsList,@PreparedNewsList,1,1,1);


	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('GenerateSystemModules',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH


			--GenerateServerModules
	BEGIN TRY  
		DECLARE db_cursor14 CURSOR LOCAL FOR SELECT sm.[InheritedPageType], sm.[Name], sm.[Description]
		FROM [dbo].[ServerModuleAndServiceList] sm
		ORDER BY sm.[InheritedPageType];


		SET @PreparedNewsList = char(13) + '# Seznam Implementovaných Modulů Serveru,   
		' + char(13) + char(13) + 'Do serveru lze zaimplementovat téměř neomezeně funkcionalit. Sám jich už má přehršel.
Je tu ale nová Dynamická agenda kde se nastavují přístupy k zaimplementovaným modulům, nebo je možné tvořit vlastní webové moduly
jako jednostánkové weby pro určitý Účel. ke snadnosti slouží knihovny kódů pomocí kterých tyto moduly můžete doslova Generovat
' + char(13);

		OPEN db_cursor14  
		FETCH NEXT FROM db_cursor14 INTO @TargetType, @StatusType, @Documentation
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
			IF(@PrewTargetType <> @TargetType) BEGIN
				SET @PreparedNewsList += + char(13) + '
---   
' + char(13) + '# ' + @TargetType + '         
' + char(13);
			END
			
			SET @PreparedNewsList +='  **' + @StatusType + '**
' + char(13) + '*' + @Documentation  + '*' + char(13) + '
' + char(13);
			
			SET @PrewTargetType = @TargetType;
			FETCH NEXT FROM db_cursor14 INTO @TargetType, @StatusType, @Documentation
		END;
		CLOSE db_cursor14;
		DEALLOCATE db_cursor14;


		--SAVE GenerateServerModules
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(2,'Seznam Impl. Modulů Serveru','Do serveru lze zaimplementovat téměř neomezeně funkcionalit. Sám jich už má přehršel.
Je tu ale nová Dynamická agenda kde se nastavují přístupy k zaimplementovaným modulům, nebo je možné tvořit vlastní webové moduly
jako jednostánkové weby pro určitý Účel. ke snadnosti slouží knihovny kódů pomocí kterých tyto moduly můžete doslova Generovat',40,@PreparedNewsList,@PreparedNewsList,1,1,1);


	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('GenerateServerModules',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);
	END CATCH


	--GenerateMicroDials
	BEGIN TRY  
		DECLARE db_cursor15 CURSOR LOCAL FOR SELECT sm.ItemsGroup, sm.[Name], sm.[Description]
		FROM [dbo].[SolutionMixedEnumList] sm
		ORDER BY sm.ItemsGroup;


		SET @PreparedNewsList = char(13) + '# Mikročíselníky unikátních Funcionalit,   
		' + char(13) + char(13) + 'Mikročíselník vzniknul jako úsporatabulek pro Výběry z malinkých seznamů. Tyto Seznamy
jsou ale implementované funkcionality, Systému i Serveru to z něho Dělá naprosto mimořádnou Agendu. Zde je Vypsán jeho
Obsah.
' + char(13);

		OPEN db_cursor15  
		FETCH NEXT FROM db_cursor15 INTO @TargetType, @StatusType, @Documentation
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
			IF(@PrewTargetType <> @TargetType) BEGIN
				SET @PreparedNewsList += + char(13) + '
---   
' + char(13) + '# ' + @TargetType + '         
' + char(13);
			END
			
			SET @PreparedNewsList +='  **' + @StatusType + '**
' + char(13) + '*' + @Documentation  + '*' + char(13) + '
' + char(13);
			
			SET @PrewTargetType = @TargetType;
			FETCH NEXT FROM db_cursor15 INTO @TargetType, @StatusType, @Documentation
		END;
		CLOSE db_cursor15;
		DEALLOCATE db_cursor15;


		--SAVE GenerateMicroDials
		INSERT INTO [dbo].[DocSrvDocumentationList] ([DocumentationGroupId],[Name], [Description],[Sequence],[MdContent],[HtmlContent],[UserId],[Active],[AutoVersion])
		VALUES(1,'Mikročíselníky unikátní Funkce','Mikročíselník vzniknul jako úspora tabulek pro Výběry z malinkých seznamů. Tyto Seznamy
jsou ale implementované funkcionality, Systému i Serveru to z něho Dělá naprosto mimořádnou Agendu. Zde je Vypsán jeho
Obsah.',70,@PreparedNewsList,@PreparedNewsList,1,1,1);

	SELECT 'Generate AutoDocs From Descriptions Completed' as 'MessageList';

	END TRY  
	BEGIN CATCH  
		SELECT @Error = CONCAT('GenerateMicroDials',' ,ErrNo:',CAST(ERROR_NUMBER() AS VARCHAR),' ,Message:',ERROR_MESSAGE()); 
		INSERT INTO [dbo].[SolutionFailList] ([Source],[Message]) VALUES('Database', @Error);

		SELECT @Error as 'MessageList';
	END CATCH


END;
