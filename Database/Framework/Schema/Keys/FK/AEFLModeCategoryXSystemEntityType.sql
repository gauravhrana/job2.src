
ALTER TABLE dbo.AEFLModeCategoryXSystemEntityType
	ADD CONSTRAINT FK_AEFLModeCategoryXSystemEntityType_AEFLModeCategory FOREIGN KEY
	(
		AEFLModeCategoryId
	)
	REFERENCES dbo.ApplicationEntityFieldLabelModeCategory
	(
		AEFLModeCategoryId
	)
GO


ALTER TABLE dbo.AEFLModeCategoryXSystemEntityType
	ADD CONSTRAINT FK_AEFLModeCategoryXSystemEntityType_SystemEntityType FOREIGN KEY
	(
		SystemEntityTypeId
	)
	REFERENCES dbo.SystemEntityType
	(
		SystemEntityTypeId
	)
GO


