IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.TimeZone')
	AND		name	= N'UQ_TimeZone_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TimeZoneOperation_Name'
	ALTER TABLE dbo.TimeZone
		DROP CONSTRAINT	UQ_TimeZone_ApplicationId_Name
END
GO

ALTER TABLE dbo.TimeZone
	ADD CONSTRAINT UQ_TimeZone_ApplicationId_Name UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	Name
	)
GO
