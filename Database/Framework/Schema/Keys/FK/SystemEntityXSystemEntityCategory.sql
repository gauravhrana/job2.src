ALTER TABLE dbo.SystemEntityXSystemEntityCategory
	ADD CONSTRAINT FK_SystemEntityXSystemEntityCategory_SystemEntityCategory FOREIGN KEY
	(
		SystemEntityCategoryId
	)
	REFERENCES SystemEntityCategory
	(
		SystemEntityCategoryId
	)
GO

ALTER TABLE dbo.SystemEntityXSystemEntityCategory
	ADD CONSTRAINT FK_SystemEntityXSystemEntityCategory_SystemEntityType FOREIGN KEY
	(
		SystemEntityId
	)
	REFERENCES dbo.SystemEntityType
	(
		SystemEntityTypeId
	)
GO