IF OBJECT_ID ('dbo.zApplicationEntityParentalHierarchy') IS NOT NULL
	DROP TABLE dbo.zApplicationEntityParentalHierarchy
GO

CREATE TABLE dbo.zApplicationEntityParentalHierarchy
(
	ApplicationEntityParentalHierarchyId	INT				NOT NULL,
	ApplicationId							INT				NOT NULL,
	Name									VARCHAR (50)	NOT NULL,
	Description								VARCHAR (250)	NOT NULL,
	SortOrder								INT				NOT NULL,
)
GO
