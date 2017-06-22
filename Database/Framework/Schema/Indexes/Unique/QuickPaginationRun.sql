IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.QuickPaginationRun')
	AND		name	= N'UQ_QuickPaginationRun_ApplicationId_SystemEntityTypeId_SortClause_WhereClause_ExpirationTime'
)
BEGIN
	PRINT	'Dropping UQ_QuickPaginationRun_ApplicationId_SystemEntityTypeId_SortClause_WhereClause_ExpirationTime'
	ALTER TABLE dbo.QuickPaginationRun
		DROP CONSTRAINT	UQ_QuickPaginationRun_ApplicationId_SystemEntityTypeId_SortClause_WhereClause_ExpirationTime
END
GO

ALTER TABLE dbo.QuickPaginationRun
	ADD CONSTRAINT UQ_QuickPaginationRun_ApplicationId_SystemEntityTypeId_SortClause_WhereClause_ExpirationTime UNIQUE NONCLUSTERED
	(
			ApplicationId	
		,	SystemEntityTypeId			
		,	SortClause
		,	WhereClause
		,	ExpirationTime
	)
GO
