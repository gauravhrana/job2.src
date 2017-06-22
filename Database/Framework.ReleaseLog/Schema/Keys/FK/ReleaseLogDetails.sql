ALTER TABLE dbo.ReleaseLogDetail
	ADD CONSTRAINT FK_ReleaseLogDetail_ReleaseLog FOREIGN KEY
	(
		ReleaseLogId
	)
	REFERENCES ReleaseLog
	(
		ReleaseLogId
	)
GO

ALTER TABLE dbo.ReleaseLogDetail
	ADD CONSTRAINT FK_ReleaseLogDetail_ReleaseIssueType FOREIGN KEY
	(
		ReleaseIssueTypeId
	)
	REFERENCES dbo.ReleaseIssueType
	(
		ReleaseIssueTypeId
	)
GO

ALTER TABLE dbo.ReleaseLogDetail
	ADD CONSTRAINT FK_ReleaseLogDetail_ReleasePublishCategory FOREIGN KEY
	(
		ReleasePublishCategoryId
	)
	REFERENCES dbo.ReleasePublishCategory
	(
		ReleasePublishCategoryId
	)
GO

ALTER TABLE dbo.ReleaseLogDetail
	ADD CONSTRAINT FK_ReleaseLogDetail_Module FOREIGN KEY
	(
		ModuleId
	)
	REFERENCES dbo.Module
	(
		ModuleId
	)
GO

ALTER TABLE dbo.ReleaseLogDetail
	ADD CONSTRAINT FK_ReleaseLogDetail_ReleaseFeature FOREIGN KEY
	(
		ReleaseFeatureId
	)
	REFERENCES dbo.ReleaseFeature
	(
		ReleaseFeatureId
	)
GO


