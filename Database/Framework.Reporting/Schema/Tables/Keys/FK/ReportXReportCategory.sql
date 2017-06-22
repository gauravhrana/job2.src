ALTER TABLE dbo.ReportXReportCategory
	ADD CONSTRAINT FK_ReportXReportCategory_ReportCategoryId FOREIGN KEY
	(
		ReportCategoryId
	)
	REFERENCES ReportCategory
	(
		ReportCategoryId
	)
GO

ALTER TABLE dbo.ReportXReportCategory
	ADD CONSTRAINT FK_ReportXReportCategory_ReportId FOREIGN KEY
	(
		ReportId
	)
	REFERENCES dbo.Report
	(
		ReportId
	)
GO