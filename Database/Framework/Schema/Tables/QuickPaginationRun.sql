IF OBJECT_ID ('dbo.QuickPaginationRun') IS NOT NULL
	DROP TABLE dbo.QuickPaginationRun
GO

CREATE TABLE dbo.QuickPaginationRun
(
		QuickPaginationRunId	INT				NOT NULL	
	,	ApplicationId			INT				NOT NULL
	,	ApplicationUserId		INT				NOT NULL	
	,	SystemEntityTypeId		INT				NOT NULL	
	,	SortClause				VARCHAR(50)		NOT NULL
	,	WhereClause				VARCHAR(500)	NOT NULL
	,	ExpirationTime			DECIMAL(15,0)	NOT NULL
)
GO