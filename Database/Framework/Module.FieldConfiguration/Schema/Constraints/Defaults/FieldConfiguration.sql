IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FieldConfiguration_DisplayColumn'
)

ALTER TABLE dbo.FieldConfiguration
	ADD CONSTRAINT DF_FieldConfiguration_DisplayColumn		DEFAULT 1 		FOR DisplayColumn
GO

IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_FieldConfiguration_CellCount'
)

ALTER TABLE dbo.FieldConfiguration
	ADD CONSTRAINT DF_FieldConfiguration_CellCount		DEFAULT 3 		FOR CellCount
GO
