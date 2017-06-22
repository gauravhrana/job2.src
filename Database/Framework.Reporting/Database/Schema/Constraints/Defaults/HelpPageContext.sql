IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_HelpPageContext_Description'
)

ALTER TABLE dbo.HelpPageContext
	ADD CONSTRAINT DF_HelpPageContext_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_HelpPageContext_SortOrder'
)

ALTER TABLE dbo.HelpPageContext
	ADD CONSTRAINT DF_HelpPageContext_SortOrder		DEFAULT 1000		FOR SortOrder
GO
