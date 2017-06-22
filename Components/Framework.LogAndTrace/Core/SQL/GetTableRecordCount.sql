	
	
SELECT	COUNT(*) 
FROM	dbo.@SourceTableName@ a
WHERE	a.[@SourceColumnName@]	<	ISNULL('@RecordDate@', a.[@SourceColumnName@])