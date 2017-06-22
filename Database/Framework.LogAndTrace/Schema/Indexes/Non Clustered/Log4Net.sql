
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[Log4Net]')
	AND		name	= N'IX_Log4Net_Date_ApplicationId'
)
BEGIN
	PRINT	'Dropping IX_Log4Net_Date_ApplicationId'	
	DROP	INDEX	IX_Log4Net_Date_ApplicationId
		ON	dbo.Log4Net WITH ( ONLINE = OFF )
END
GO

CREATE NONCLUSTERED INDEX IX_Log4Net_Date_ApplicationId
	ON dbo.Log4Net
(
		ApplicationId ASC,
		[Date]
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

