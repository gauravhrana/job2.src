IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TimeZone_Description'
)

ALTER TABLE dbo.TimeZone
	ADD CONSTRAINT DF_TimeZone_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TimeZone_SortOrder'
)

ALTER TABLE dbo.TimeZone
	ADD CONSTRAINT DF_TimeZone_SortOrder		DEFAULT 1000		FOR SortOrder
GO
