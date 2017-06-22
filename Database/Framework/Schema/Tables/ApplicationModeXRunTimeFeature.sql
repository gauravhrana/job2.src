IF OBJECT_ID ('dbo.ApplicationModeXRunTimeFeature') IS NOT NULL
	DROP TABLE dbo.ApplicationModeXRunTimeFeature
GO

CREATE TABLE dbo.ApplicationModeXRunTimeFeature
(
		ApplicationModeXRunTimeFeatureId	INT		NOT NULL
	,   ApplicationId						INT		NOT NULL 	
	,	ApplicationModeId					INT		NOT NULL		
	,	RunTimeFeatureId					INT		NOT NULL
);

