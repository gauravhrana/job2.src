IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_SystemEntityType_PrimaryDatabase'
)

ALTER TABLE dbo.SystemEntityType
	ADD CONSTRAINT DF_SystemEntityType_PrimaryDatabase	DEFAULT 'Configuration' FOR PrimaryDatabase
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_SystemEntityType_CreatedDate'
)

ALTER TABLE dbo.SystemEntityType
	ADD CONSTRAINT DF_SystemEntityType_CreatedDate	DEFAULT GetDate() FOR CreatedDate
GO
