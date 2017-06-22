IF OBJECT_ID ('dbo.FeatureOwnerStatus') IS NOT NULL
	DROP TABLE dbo.FeatureOwnerStatus
GO

CREATE TABLE dbo.FeatureOwnerStatus
(
		FeatureOwnerStatusId	INT				NOT NULL
	,   ApplicationId			INT				NOT NULL 	
	,	Name					VARCHAR (50)	NOT NULL	
	,	Description				VARCHAR (50)	NOT NULL	
	,	SortOrder				INT				NOT NULL
);

