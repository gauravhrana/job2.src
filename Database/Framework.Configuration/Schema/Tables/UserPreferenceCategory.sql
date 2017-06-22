IF OBJECT_ID ('dbo.UserPreferenceCategory') IS NOT NULL
	DROP TABLE dbo.UserPreferenceCategory
GO

CREATE TABLE dbo.UserPreferenceCategory
(
		UserPreferenceCategoryId	INT				NOT NULL	
	,	ApplicationId				INT				NOT NULL
	,	Name						VARCHAR (100)	NOT NULL	
	,	Description					VARCHAR (500)	NOT NULL	
	,	SortOrder					INT				NOT NULL
)
GO
