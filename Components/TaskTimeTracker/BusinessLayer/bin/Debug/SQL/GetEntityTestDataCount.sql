DECLARE @DBName			AS	VARCHAR(50)
DECLARE @Count			AS	INT	
DECLARE @sql			AS  NVARCHAR(512)	
DECLARE @existSql		AS  NVARCHAR(512)	
DECLARE @existColumn	AS  NVARCHAR(512)	
DECLARE @existAppIdColumn	AS  NVARCHAR(512)	
DECLARE @rec			AS	INT	
DECLARE @tolColumn		AS	INT	
DECLARE @tolAppColumn	AS	INT	

SELECT	@DBName				= a.ConnectionKeyName
FROM	Configuration.dbo.SetUpConfiguration a
WHERE	a.EntityName		= '@EntityName@'

IF EXISTS(SELECT name FROM master.sys.databases WHERE name = @DBName)
BEGIN
	SET		@existSql =  N'SELECT @rec=count(*) '
				+	' FROM ' + @DBName + '.SYS.TABLES where name =' + '''@EntityName@''' 

	EXEC	sp_executesql @existQuery = @existSql,@params = N'@rec INT OUTPUT', @rec = @rec OUTPUT

	IF  (@rec>0)
	BEGIN	
			SET @existColumn = N'SELECT @tolColumn=Count(*) FROM '+ 
				@DBName + '.INFORMATION_SCHEMA.Columns  WHERE COLUMN_NAME='+ '''@EntityName@Id''' + 
				' AND TABLE_NAME = '+'''@EntityName@''' 				
				
			EXEC	sp_executesql @existQCol = @existColumn,@params = N'@tolColumn INT OUTPUT', @tolColumn = @tolColumn OUTPUT
			
			SET @existAppIdColumn = N'SELECT @tolAppColumn=Count(*) FROM '+ 
				@DBName + '.INFORMATION_SCHEMA.Columns  WHERE COLUMN_NAME='+ '''ApplicationId''' + 
				' AND TABLE_NAME = '+'''@EntityName@''' 				

			EXEC	sp_executesql @existACol = @existAppIdColumn,@params = N'@tolAppColumn INT OUTPUT', @tolAppColumn = @tolAppColumn OUTPUT

			IF(@tolAppColumn>0)
			BEGIN
				IF(@tolColumn>0)
				BEGIN
					SET		@sql =  N'SELECT @Count = Count(@EntityName@Id) '
						+	' FROM ' + @DBName + '.dbo.@EntityName@ '
						+	' WHERE @EntityName@Id < 0 AND '+ 'ApplicationId = @ApplicationId@'
				
					EXEC	sp_executesql 
							@query = @sql, 
							@params = N'@Count INT OUTPUT', 
							@Count = @Count OUTPUT 

					SELECT @Count
				END
			END
	END

END

