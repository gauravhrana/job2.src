IF OBJECT_ID ('dbo.FieldConfiguration') IS NOT NULL
	DROP TABLE dbo.FieldConfiguration
GO

CREATE TABLE dbo.FieldConfiguration 
(
		FieldConfigurationId			INT IDENTITY(1,1)	NOT NULL
	,	ApplicationId					INT					NOT NULL
	,	Name							VARCHAR(50)			NOT NULL
	,	Value							VARCHAR(50)			NOT NULL
	,	SystemEntityTypeId				INT					NOT NULL
	,	FieldConfigurationModeId		INT					NOT NULL
	,	Width							NUMERIC(7, 2)		NOT NULL
	,	Formatting						VARCHAR(50)			NOT NULL
	,	ControlType						VARCHAR(50)			NOT NULL
	,	HorizontalAlignment				VARCHAR(50)			NOT NULL
	,	GridViewPriority				INT					NOT NULL	DEFAULT		-1
	,	DetailsViewPriority				INT					NOT NULL	DEFAULT		-1
	,	DisplayColumn					INT					NOT NULL
	,	CellCount						INT					NOT NULL
);

