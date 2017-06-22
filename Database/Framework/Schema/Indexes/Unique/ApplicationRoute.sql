IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ApplicationRoute')
	AND		name	= N'UQ_ApplicationRoute_RouteName_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationRoute_RouteName_ApplicationId'
	ALTER TABLE dbo.ApplicationRoute
		DROP CONSTRAINT	UQ_ApplicationRoute_RouteName_ApplicationId
END
GO

ALTER TABLE dbo.ApplicationRoute
	ADD CONSTRAINT UQ_ApplicationRoute_RouteName_ApplicationId UNIQUE NONCLUSTERED
	(
			RouteName
		,	ApplicationId
		
	)
GO
