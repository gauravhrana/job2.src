IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[UserPreferenceCategory]')
	AND		name	= N'UQ_UserPreferenceCategory_Name'
)
BEGIN
	PRINT	'Dropping UQ_UserPreferenceCategory_Name'
	ALTER	TABLE dbo.UserPreferenceCategory
	DROP	CONSTRAINT	UQ_UserPreferenceCategory_Name
END
GO

ALTER TABLE dbo.UserPreferenceCategory
ADD CONSTRAINT UQ_UserPreferenceCategory_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
