IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_QuickPaginationRun_SortClause'
)

ALTER TABLE dbo.QuickPaginationRun
	ADD CONSTRAINT DF_QuickPaginationRun_SortClause		DEFAULT '' 		FOR SortClause
GO

IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_QuickPaginationRun_WhereClause'
)

ALTER TABLE dbo.QuickPaginationRun
	ADD CONSTRAINT DF_QuickPaginationRun_WhereClause		DEFAULT '' 		FOR WhereClause
GO

