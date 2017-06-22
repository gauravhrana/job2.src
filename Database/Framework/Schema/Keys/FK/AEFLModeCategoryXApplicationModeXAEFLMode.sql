ALTER TABLE dbo.AEFLModeCategoryXApplicationModeXAEFLMode
	ADD CONSTRAINT FK_AEFLModeCategoryXApplicationModeXAEFLMode_AEFLModeCategory FOREIGN KEY
	(
		AEFLModeCategoryId
	)
	REFERENCES ApplicationEntityFieldLabelModeCategory
	(
		AEFLModeCategoryId
	)
GO

ALTER TABLE dbo.AEFLModeCategoryXApplicationModeXAEFLMode
	ADD CONSTRAINT FK_AEFLModeCategoryXApplicationModeXAEFLMode_ApplicationMode FOREIGN KEY
	(
		ApplicationModeId
	)
	REFERENCES ApplicationMode
	(
		ApplicationModeId
	)
GO

ALTER TABLE dbo.AEFLModeCategoryXApplicationModeXAEFLMode
	ADD CONSTRAINT FK_AEFLModeCategoryXApplicationModeXAEFLMode_ApplicationEntityFieldLabelMode FOREIGN KEY
	(
		ApplicationEntityFieldLabelModeId
	)
	REFERENCES ApplicationEntityFieldLabelMode
	(
		ApplicationEntityFieldLabelModeId
	)
GO


