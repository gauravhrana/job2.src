	
DECLARE @DBName			AS	VARCHAR(50)
DECLARE @MinValue		AS	INT	
DECLARE @sql			AS  NVARCHAR(512)	
	
SELECT	@DBName				= a.ConnectionKeyName
FROM	Configuration.dbo.SetUpConfiguration a
WHERE	a.EntityName		= '@EntityName@'

SET		@sql =  N'SELECT @MinValue = MIN(@EntityName@Id) '
			+	' FROM ' + @DBName + '.dbo.@EntityName@ '

EXEC	sp_executesql 
		@query = @sql, 
		@params = N'@MinValue INT OUTPUT', 
		@MinValue = @MinValue OUTPUT 

SELECT @MinValue