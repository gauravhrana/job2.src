IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_MenuDisplayName_IsDefault'
)

ALTER TABLE dbo.MenuDisplayName
	ADD CONSTRAINT DF_MenuDisplayName_IsDefault		DEFAULT 0 		FOR IsDefault
GO
