IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FieldConfigurationModeAccessMode_Description'
)

ALTER TABLE dbo.FieldConfigurationModeAccessMode
	ADD CONSTRAINT DF_FieldConfigurationModeAccessMode_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FieldConfigurationModeAccessMode_SortOrder'
)

ALTER TABLE dbo.FieldConfigurationModeAccessMode
	ADD CONSTRAINT DF_FieldConfigurationModeAccessMode_SortOrder		DEFAULT 100 		FOR SortOrder
GO
