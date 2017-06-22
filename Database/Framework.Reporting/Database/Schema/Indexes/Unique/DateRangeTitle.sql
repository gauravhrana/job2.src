IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.DateRangeTitle')
	AND		name	= N'UQ_DateRangeTitle_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_DateRangeTitle_Name_ApplicationId'
	ALTER TABLE dbo.DateRangeTitle
		DROP CONSTRAINT	UQ_DateRangeTitle_Name_ApplicationId
END
GO

ALTER TABLE dbo.DateRangeTitle
	ADD CONSTRAINT UQ_DateRangeTitle_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
