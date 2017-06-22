IF OBJECT_ID ('dbo.UserPreference') IS NOT NULL
	DROP TABLE dbo.UserPreference
GO

CREATE TABLE dbo.UserPreference
	(
			UserPreferenceId			INT				NOT NULL
		,	ApplicationUserId			INT				NOT NULL
		,	UserPreferenceCategoryId	INT				NOT NULL
		,	UserPreferenceKeyId			INT				NOT NULL
		,	Value						VARCHAR (50)	NOT NULL
		,	DataTypeId					INT				NOT NULL
		,	ApplicationId				INT				NOT NULL
	)
GO
