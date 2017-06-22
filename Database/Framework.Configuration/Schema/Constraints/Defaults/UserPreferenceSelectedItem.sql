
IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_UserPreferenceSelectedItem_SortOrder'
)

ALTER TABLE dbo.UserPreferenceSelectedItem
	ADD CONSTRAINT DF_UserPreferenceSelectedItem_SortOrder		DEFAULT 1000		FOR SortOrder
GO
