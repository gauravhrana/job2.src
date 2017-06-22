  	
DECLARE @sql	AS  NVARCHAR(512)
 
SET	@sql =		N'SELECT * '
			+	'INTO @BackupTableName@ ' 
			+	'FROM dbo.Log4Net '
			+	'WHERE [Date] <  ''@RecordDate@'' ' 
						
EXEC sp_executesql @sql 

SET	@sql = 'DELETE FROM dbo.Log4Net Where [Date] < ''@RecordDate@'' '

EXEC sp_executesql @sql 