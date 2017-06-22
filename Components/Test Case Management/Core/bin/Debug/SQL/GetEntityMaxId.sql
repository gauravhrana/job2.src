DECLARE @existSql		AS  NVARCHAR(512)	
DECLARE @rec			AS	INT		
DECLARE @DBName			AS	VARCHAR(50)
DECLARE @MaxValue		AS	INT	
DECLARE @sql			AS  NVARCHAR(512)	
	
SELECT	@DBName				= a.ConnectionKeyName
FROM	Configuration.dbo.SetUpConfiguration a
WHERE	a.EntityName		= '@EntityName@'

IF EXISTS(SELECT name FROM master.sys.databases WHERE name = @DBName)
BEGIN
	SET		@existSql =  N'SELECT @rec=count(*) '
				+	' FROM ' + @DBName + '.SYS.TABLES where name =' + '''@EntityName@''' 

	EXEC	sp_executesql @existQuery = @existSql,@params = N'@rec INT OUTPUT', @rec = @rec OUTPUT

	IF  (@rec>0 AND '@EntityName@' <> 'Log4Net' AND '@EntityName@' <> 'DatabaseChangeLog')
	BEGIN	

SET		@sql =  N'SELECT @MaxValue = Max(@EntityName@Id) '
			+	' FROM ' + @DBName + '.dbo.@EntityName@ '

EXEC	sp_executesql 
		@query = @sql, 
		@params = N'@MaxValue INT OUTPUT', 
		@MaxValue = @MaxValue OUTPUT 

SELECT @MaxValue

END

END