IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[UserPreferenceSelectedItem]')
	AND		name	= N'UQ_UserPreferenceSelectedItem_ApplicationId_ApplicationUserId_ParentKey_Value'
)
BEGIN
	PRINT	'Dropping UQ_UserPreferenceSelectedItem_ApplicationId_ApplicationUserId_ParentKey_Value'
	ALTER	TABLE dbo.UserPreferenceSelectedItem
	DROP	CONSTRAINT	UQ_UserPreferenceSelectedItem_ApplicationId_ApplicationUserId_ParentKey_Value
END
GO

ALTER TABLE dbo.UserPreferenceSelectedItem
ADD CONSTRAINT UQ_UserPreferenceSelectedItem_ApplicationId_ApplicationUserId_ParentKey_Value UNIQUE NONCLUSTERED
(
		ApplicationId
	,	ApplicationUserId
	,	ParentKey
	,	Value
)
GO
