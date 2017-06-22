IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_UserLoginStatus_Description'
)

ALTER TABLE dbo.UserLoginStatus
	ADD CONSTRAINT DF_UserLoginStatus_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_UserLoginStatus_SortOrder'
)

ALTER TABLE dbo.UserLoginStatus
	ADD CONSTRAINT DF_UserLoginStatus_SortOrder		DEFAULT 1000		FOR SortOrder
GO
