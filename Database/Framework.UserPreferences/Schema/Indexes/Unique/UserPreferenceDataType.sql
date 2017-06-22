IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[UserPreferenceDataType]')
	AND		name	= N'UQ_UserPreferenceDataType_Name'
)
BEGIN
	PRINT	'Dropping UQ_UserPreferenceDataType_Name'
	ALTER	TABLE dbo.UserPreferenceDataType
	DROP	CONSTRAINT	UQ_UserPreferenceDataType_Name
END
GO

ALTER TABLE dbo.UserPreferenceDataType
ADD CONSTRAINT UQ_UserPreferenceDataType_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
