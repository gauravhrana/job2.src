IF OBJECT_ID ('dbo.UserPreferenceDataType') IS NOT NULL
	DROP TABLE dbo.UserPreferenceDataType
GO

CREATE TABLE dbo.UserPreferenceDataType
(
		UserPreferenceDataTypeId	INT				NOT NULL	
	,	ApplicationId				INT				NOT NULL
	,	Name						VARCHAR (50)	NOT NULL	
	,	Description					VARCHAR (50)	NOT NULL	
	,	SortOrder					INT				NOT NULL
)
GO
