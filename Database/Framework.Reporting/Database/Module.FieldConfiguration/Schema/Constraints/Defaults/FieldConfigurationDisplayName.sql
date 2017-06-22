IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FieldConfigurationDisplayName_IsDefault'
)

ALTER TABLE dbo.FieldConfigurationDisplayName
	ADD CONSTRAINT DF_FieldConfigurationDisplayName_IsDefault		DEFAULT 0 		FOR IsDefault
GO
