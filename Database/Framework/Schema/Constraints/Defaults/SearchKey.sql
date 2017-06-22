IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_SearchKey_Description'
)

ALTER TABLE dbo.SearchKey
	ADD CONSTRAINT DF_SearchKey_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_SearchKey_SortOrder'
)

ALTER TABLE dbo.SearchKey
	ADD CONSTRAINT DF_SearchKey_SortOrder		DEFAULT 1000		FOR SortOrder
GO
