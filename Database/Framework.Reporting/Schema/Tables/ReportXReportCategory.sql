IF OBJECT_ID ('dbo.ReportXReportCategory') IS NOT NULL
	DROP TABLE dbo.ReportXReportCategory
GO


CREATE TABLE dbo.ReportXReportCategory
(
		ReportXReportCategoryId		INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	ReportId					INT		NOT NULL
	,	ReportCategoryId			INT		NOT NULL
	
)
GO




