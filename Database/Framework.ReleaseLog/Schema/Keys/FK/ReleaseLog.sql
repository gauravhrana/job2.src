ALTER TABLE dbo.ReleaseLog
	ADD CONSTRAINT FK_ReleaseLog_ReleaseLogStatus FOREIGN KEY
	(
		ReleaseLogStatusId
	)
	REFERENCES dbo.ReleaseLogStatus
	(
		ReleaseLogStatusId
	)
GO