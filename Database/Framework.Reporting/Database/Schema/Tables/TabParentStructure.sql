IF OBJECT_ID ('dbo.TabParentStructure') IS NOT NULL
	DROP TABLE dbo.TabParentStructure
GO

CREATE TABLE dbo.TabParentStructure
(
			TabParentStructureId	INT				NOT NULL
		,	ApplicationId			INT				NOT NULL
		,	Name					VARCHAR(50)		NOT NULL
		,	Description				VARCHAR(50)		NOT NULL
		,	SortOrder				INT				NOT NULL
		,	IsAllTab				INT				NOT NULL
)


