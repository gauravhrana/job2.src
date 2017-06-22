IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ConnectionString_Description'
)

ALTER TABLE dbo.ConnectionString
	ADD CONSTRAINT DF_ConnectionString_Description		DEFAULT '' 		FOR Description
GO
