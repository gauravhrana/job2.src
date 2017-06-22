IF OBJECT_ID ('dbo.ModuleOwner') IS NOT NULL
	DROP TABLE dbo.ModuleOwner
GO

CREATE TABLE dbo.ModuleOwner
(
		ModuleOwnerId			INT				NOT NULL
	,   ApplicationId			INT				NOT NULL 
	,	ModuleId				INT				NOT NULL	
	,	DeveloperRoleId			INT				NOT NULL	
	,	Developer				VARCHAR (50)	NOT NULL	
	,	FeatureOwnerStatusId	INT				NOT NULL
);

