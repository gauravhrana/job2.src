﻿
IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[Log4Net]')
	AND		name	= N'PK_Log4Net'
)
BEGIN
	PRINT	'Dropping PK_Log4Net'	
	DROP	INDEX	PK_Log4Net
		ON	dbo.Log4Net WITH ( ONLINE = OFF )
END
GO

CREATE CLUSTERED INDEX PK_Log4Net
	ON dbo.Log4Net
(
		Id asc
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

