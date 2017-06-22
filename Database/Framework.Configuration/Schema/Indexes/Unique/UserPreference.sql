IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[UserPreference]')
	AND		name	= N'UQ_UserPreference_COMBO'
)
BEGIN
	PRINT	'Dropping UQ_UserPreference_COMBO'
	ALTER	TABLE dbo.UserPreference
	DROP	CONSTRAINT	UQ_UserPreference_COMBO
END
GO

ALTER TABLE dbo.UserPreference
ADD CONSTRAINT UQ_UserPreference_COMBO UNIQUE NONCLUSTERED
(
		ApplicationId
	,	ApplicationUserId
	,	UserPreferenceCategoryId
	,	UserPreferenceKeyId
)
GO
