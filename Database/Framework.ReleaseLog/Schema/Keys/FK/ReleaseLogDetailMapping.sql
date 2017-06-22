ALTER TABLE dbo.ReleaseLogDetailMapping
	ADD CONSTRAINT FK_ReleaseLogDetailMapping_ReleaseLogDetail_ParentReleaseLogDetailId FOREIGN KEY
	(
		ParentReleaseLogDetailId
	)
	REFERENCES ReleaseLogDetail
	(
		ReleaseLogDetailId
	)
GO

ALTER TABLE dbo.ReleaseLogDetailMapping
	ADD CONSTRAINT FK_ReleaseLogDetail_ReleaseIssueType_ChildReleaseLogDetailId FOREIGN KEY
	(
		ChildReleaseLogDetailId
	)
	REFERENCES dbo.ReleaseLogDetail
	(
		ReleaseLogDetailId
	)
GO