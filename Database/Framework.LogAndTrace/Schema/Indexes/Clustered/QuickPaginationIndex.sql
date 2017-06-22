
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[QuickPaginationIndex]')
	AND		name	= N'INDEX_QuickPaginationIndex_QuickPaginationRunId_RowNumber'
)
BEGIN
	PRINT	'Dropping INDEX_QuickPaginationIndex_QuickPaginationRunId_RowNumber'	
	DROP	INDEX	INDEX_QuickPaginationIndex_QuickPaginationRunId_RowNumber
		ON	dbo.QuickPaginationIndex WITH ( ONLINE = OFF )
END
GO

CREATE CLUSTERED INDEX INDEX_QuickPaginationIndex_QuickPaginationRunId_RowNumber
	ON dbo.QuickPaginationIndex
(
		QuickPaginationRunId ASC
	,	RowNumber
)
WITH 
	(
			PAD_INDEX				= OFF
		,	STATISTICS_NORECOMPUTE  = OFF
		,	SORT_IN_TEMPDB			= OFF
		,	IGNORE_DUP_KEY			= OFF
		,	DROP_EXISTING			= OFF
		,	ONLINE					= OFF
		,	ALLOW_ROW_LOCKS			= ON
		,	ALLOW_PAGE_LOCKS		= ON
	) ON [PRIMARY]

GO

