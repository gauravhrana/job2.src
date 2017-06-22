IF OBJECT_ID ('dbo.ApplicationEntityFieldLabel') IS NOT NULL
	DROP TABLE dbo.ApplicationEntityFieldLabel
GO

CREATE TABLE dbo.ApplicationEntityFieldLabel 
(
		ApplicationEntityFieldLabelId	int IDENTITY(1,1)	NOT NULL
	,	ApplicationId					int					NOT NULL
	,	Name							varchar(50)			NOT NULL
	,	Value							varchar(50)			NOT NULL
	,	SystemEntityTypeId				int					NOT NULL
	,	Width							numeric(7, 2)		NOT NULL
	,	Formatting						varchar(50)			NOT NULL
	,	ControlType						varchar(50)			NOT NULL
	,	HorizontalAlignment				varchar(50)			NOT NULL
	,	GridViewPriority				int					NOT NULL	DEFAULT		-1
	,	DetailsViewPriority				int					NOT NULL	DEFAULT		-1
);

