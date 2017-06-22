ALTER TABLE dbo.AEFLModeCategoryXAEFLMode
	ADD CONSTRAINT FK_AEFLModeCategoryXAEFLMode_Application FOREIGN KEY
	(
		ApplicationId
	)
	REFERENCES Application
	(
		ApplicationId
	)
GO

ALTER TABLE dbo.AEFLModeCategoryXAEFLMode
	ADD CONSTRAINT FK_AEFLModeCategoryXAEFLMode_AEFLModeCategory FOREIGN KEY
	(
		AEFLModeCategoryId
	)
	REFERENCES dbo.ApplicationEntityFieldLabelModeCategory
	(
		AEFLModeCategoryId
	)
GO


ALTER TABLE dbo.AEFLModeCategoryXAEFLMode
	ADD CONSTRAINT FK_AEFLModeCategoryXAEFLMode_AEFLMode FOREIGN KEY
	(
		AEFLModeId
	)
	REFERENCES dbo.ApplicationEntityFieldLabelMode
	(
		ApplicationEntityFieldLabelModeId
	)
GO