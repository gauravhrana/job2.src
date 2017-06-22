IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_HelpPage_Content'
)

ALTER TABLE dbo.HelpPage
	ADD CONSTRAINT DF_HelpPage_Content		DEFAULT '' 		FOR Content
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_HelpPage_SortOrder'
)

ALTER TABLE dbo.HelpPage
	ADD CONSTRAINT DF_HelpPage_SortOrder		DEFAULT 1000		FOR SortOrder
GO
