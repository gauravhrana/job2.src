
DECLARE @DBName			AS	VARCHAR(50)
DECLARE @sql			AS  NVARCHAR(512)	
	
	
SELECT	@DBName				= a.ConnectionKeyName
FROM	Configuration.dbo.SetUpConfiguration a
WHERE	a.EntityName		= '@EntityName@'

SET		@sql =  N'SELECT * FROM ' + @DBName + '.dbo.' + '@EntityName@' 

  			
EXEC	sp_executesql 
		@query = @sql 
		
