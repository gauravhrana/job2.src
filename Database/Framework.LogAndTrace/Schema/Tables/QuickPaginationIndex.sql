IF OBJECT_ID ('dbo.QuickPaginationIndex') IS NOT NULL
	DROP TABLE dbo.QuickPaginationIndex
GO

CREATE TABLE dbo.QuickPaginationIndex
(
		QuickPaginationRunId	INT				NOT NULL	
	,	RowNumber				INT				NOT NULL	
	,	EntityKey				INT				NOT NULL
)
GO