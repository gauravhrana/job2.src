IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Country_Description'
)

ALTER TABLE dbo.Country
	ADD CONSTRAINT DF_Country_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Country_SortOrder'
)

ALTER TABLE dbo.Country
	ADD CONSTRAINT DF_Country_SortOrder		DEFAULT 1000		FOR SortOrder
GO
