IF OBJECT_ID ('dbo.UserPreferenceKey') IS NOT NULL
	DROP TABLE dbo.UserPreferenceKey
GO

CREATE TABLE dbo.UserPreferenceKey
(
		UserPreferenceKeyId		INT				NOT NULL
	,	ApplicationId			INT				NOT NULL
	,	Name					VARCHAR(50)		NOT NULL
	,	Value					VARCHAR(50)		NOT NULL
	,	DataTypeId				INT				NOT NULL
	,	Description				VARCHAR(50)		NOT NULL	
	,	SortOrder				INT				NOT NULL
)
GO
