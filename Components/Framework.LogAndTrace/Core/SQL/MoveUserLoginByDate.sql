  	
DECLARE @sql	AS  NVARCHAR(512)
 
SET	@sql =		N'SELECT * '
			+	'INTO @BackupTableName@ ' 
			+	'FROM dbo.UserLogin '
			+	'WHERE [RecordDate] <  ''@RecordDate@'' ' 
						
EXEC sp_executesql @sql 

SET	@sql = 'DELETE FROM dbo.UserLogin Where [RecordDate] < ''@RecordDate@'' '

EXEC sp_executesql @sql 