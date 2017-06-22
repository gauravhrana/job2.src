IF OBJECT_ID ('dbo.ReleaseFeature') IS NOT NULL
	DROP TABLE dbo.ReleaseFeature
GO


CREATE TABLE dbo.ReleaseFeature
(
		ReleaseFeatureId			INT				IDENTITY(1,1)	NOT NULL
	,	ApplicationId				INT								NOT NULL
	,	Name						VARCHAR (50)					NOT NULL
	,	[Description]				VARCHAR (500)					NOT NULL
	,	SortOrder					INT								NOT NULL
	,	DateCreated					DATETIME						NOT NULL
	,	DateModified				DATETIME						NOT NULL
	,	CreatedByAuditId			INT								NOT NULL
	,	ModifiedByAuditId			INT								NOT NULL
);

