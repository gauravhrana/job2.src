IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.Report')
	AND		name	= N'UQ_Report_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_Report_Name_ApplicationId'
	ALTER TABLE dbo.Report
		DROP CONSTRAINT	UQ_Report_Name_ApplicationId
END
GO

ALTER TABLE dbo.Report
	ADD CONSTRAINT UQ_Report_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
