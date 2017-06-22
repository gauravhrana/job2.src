  	
DECLARE @sql	AS  NVARCHAR(512)
 
SET	@sql =		N'SELECT * '
			+	'INTO @BackupTableName@ ' 
			+	'FROM dbo.UserLoginHistory '
			+	'WHERE [DateVisited] <  ''@RecordDate@'' ' 
						
EXEC sp_executesql @sql 

SET	@sql = 'DELETE FROM dbo.UserLoginHistory Where [DateVisited] < ''@RecordDate@'' '

EXEC sp_executesql @sql 