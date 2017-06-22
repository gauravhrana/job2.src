ALTER TABLE dbo.ApplicationModeXRunTimeFeature
	ADD CONSTRAINT FK_ApplicationModeXRunTimeFeature_ApplicationMode FOREIGN KEY
	(
		ApplicationModeId
	)
	REFERENCES dbo.ApplicationMode
	(
		ApplicationModeId 
	)
GO

ALTER TABLE dbo.ApplicationModeXRunTimeFeature
	ADD CONSTRAINT FK_ApplicationModeXRunTimeFeature_RunTimeFeature FOREIGN KEY
	(
		RunTimeFeatureId
	)
	REFERENCES dbo.RunTimeFeature
	(
		RunTimeFeatureId 
	)
GO