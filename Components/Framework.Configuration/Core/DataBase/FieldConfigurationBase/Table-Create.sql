IF OBJECT_ID ('dbo.FieldConfigurationBase') IS NOT NULL
	DROP TABLE dbo.FieldConfigurationBase
GO

CREATE TABLE dbo.FieldConfigurationBase
(
		FieldConfigurationBaseId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Value				VARCHAR(100)		NOT NULL
	,	ControlType				VARCHAR(100)		NOT NULL
	,	Formatting				VARCHAR(100)		NOT NULL
	,	Version				VARCHAR(100)		NOT NULL
	,	Width				INT		NOT NULL
)
GO
