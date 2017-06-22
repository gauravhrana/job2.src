IF OBJECT_ID ('dbo.EntityOwner') IS NOT NULL
	DROP TABLE dbo.EntityOwner
GO

CREATE TABLE dbo.EntityOwner
(
		EntityOwnerId			INT				NOT NULL
	,   ApplicationId			INT				NOT NULL 
	,	EntityId				INT				NOT NULL	
	,	DeveloperRoleId			INT				NOT NULL	
	,	Developer				VARCHAR (50)	NOT NULL	
	,	FeatureOwnerStatusId	INT				NOT NULL
);

