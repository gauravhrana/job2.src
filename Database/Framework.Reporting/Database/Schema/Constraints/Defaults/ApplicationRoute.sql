IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ApplicationRoute_Description'
)

ALTER TABLE dbo.ApplicationRoute
	ADD CONSTRAINT DF_ApplicationRoute_Description		DEFAULT '' 		FOR Description
GO