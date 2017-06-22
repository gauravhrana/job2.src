IF OBJECT_ID ('dbo.UserPreferenceSelectedItem') IS NOT NULL
	DROP TABLE dbo.UserPreferenceSelectedItem
GO

CREATE TABLE dbo.UserPreferenceSelectedItem
(
		UserPreferenceSelectedItemId	INT				IDENTITY(1, 1)	NOT NULL
	,	ApplicationId					INT								NOT NULL
	,	ApplicationUserId				INT								NOT NULL
	,	UserPreferenceKeyId				INT								NOT NULL
	,	ParentKey						VARCHAR(50)						NOT NULL
	,	Value							VARCHAR(50)						NOT NULL	
	,	SortOrder						INT								NOT NULL
)
GO
