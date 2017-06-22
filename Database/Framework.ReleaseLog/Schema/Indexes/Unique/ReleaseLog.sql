IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ReleaseLog')
	AND		name	= N'UQ_ReleaseLog_Name'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseLog_Name'
	ALTER TABLE dbo.ReleaseLog
		DROP CONSTRAINT	UQ_ReleaseLog_Name
END
GO

ALTER TABLE dbo.ReleaseLog
	ADD CONSTRAINT UQ_ReleaseLog_Name UNIQUE NONCLUSTERED
	(
			Name
		,   ApplicationId
	)
GO