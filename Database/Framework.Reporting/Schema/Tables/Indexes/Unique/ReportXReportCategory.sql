IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReportXReportCategory]')
	AND		name	= N'UQ_ReportXReportCategory_ApplicationId_ReportId_ReportCategoryId'
)
BEGIN
	PRINT	'Dropping UQ_ReportXReportCategory_ApplicationId_ReportId_ReportCategoryId'
	ALTER	TABLE dbo.ReportXReportCategory
	DROP	CONSTRAINT	UQ_ReportXReportCategory_ApplicationId_ReportId_ReportCategoryId
END
GO

ALTER TABLE dbo.ReportXReportCategory
ADD CONSTRAINT UQ_ReportXReportCategory_ApplicationId_ReportId_ReportCategoryId UNIQUE NONCLUSTERED
(
		ApplicationId
	,	ReportId 
	,	ReportCategoryId	
)
GO
