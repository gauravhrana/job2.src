IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ReportCategory')
	AND		name	= N'UQ_ReportCategory_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReportCategory_Name_ApplicationId'
	ALTER TABLE dbo.ReportCategory
		DROP CONSTRAINT	UQ_ReportCategory_Name_ApplicationId
END
GO

ALTER TABLE dbo.ReportCategory
	ADD CONSTRAINT UQ_ReportCategory_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
