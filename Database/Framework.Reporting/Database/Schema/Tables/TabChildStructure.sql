IF OBJECT_ID ('dbo.TabChildStructure') IS NOT NULL
	DROP TABLE dbo.TabChildStructure
GO

CREATE TABLE dbo.TabChildStructure
(
			TabChildStructureId		INT				NOT NULL
		,	ApplicationId			INT				NOT NULL
		,	TabParentStructureId	INT				NOT NULL
		,	Name					VARCHAR(50)		NOT NULL
		,	EntityName				VARCHAR(50)		NOT NULL
		,	SortOrder				INT				NOT NULL
		,	InnerControlPath		VARCHAR(200)	NOT NULL
)


