IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[UserPreferenceKey]')
	AND		name	= N'UQ_UserPreferenceKey_Name'
)
BEGIN
	PRINT	'Dropping UQ_UserPreferenceKey_Name'
	ALTER	TABLE dbo.UserPreferenceKey
	DROP	CONSTRAINT	UQ_UserPreferenceKey_Name
END
GO

ALTER TABLE dbo.UserPreferenceKey
ADD CONSTRAINT UQ_UserPreferenceKey_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
