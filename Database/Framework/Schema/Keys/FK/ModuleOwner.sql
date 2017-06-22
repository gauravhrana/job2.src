ALTER TABLE dbo.ModuleOwner
	ADD CONSTRAINT FK_ModuleOwner_Module FOREIGN KEY
	(
		ModuleId
	)
	REFERENCES dbo.Module
	(
		ModuleId 
	)
GO

ALTER TABLE dbo.ModuleOwner
	ADD CONSTRAINT FK_ModuleOwner_DeveloperRole FOREIGN KEY
	(
		DeveloperRoleId
	)
	REFERENCES dbo.DeveloperRole
	(
		DeveloperRoleId 
	)
GO

ALTER TABLE dbo.ModuleOwner
	ADD CONSTRAINT FK_ModuleOwner_FeatureOwnerStatus FOREIGN KEY
	(
		FeatureOwnerStatusId
	)
	REFERENCES dbo.FeatureOwnerStatus
	(
		FeatureOwnerStatusId 
	)
GO