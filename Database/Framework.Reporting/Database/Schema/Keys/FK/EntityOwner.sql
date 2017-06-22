ALTER TABLE dbo.EntityOwner
	ADD CONSTRAINT FK_EntityOwner_DeveloperRole FOREIGN KEY
	(
		DeveloperRoleId
	)
	REFERENCES dbo.DeveloperRole
	(
		DeveloperRoleId 
	)
GO

ALTER TABLE dbo.EntityOwner
	ADD CONSTRAINT FK_EntityOwner_FeatureOwnerStatus FOREIGN KEY
	(
		FeatureOwnerStatusId
	)
	REFERENCES dbo.FeatureOwnerStatus
	(
		FeatureOwnerStatusId 
	)
GO