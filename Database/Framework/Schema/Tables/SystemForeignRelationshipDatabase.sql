IF OBJECT_ID ('dbo.SystemForeignRelationshipDatabase') IS NOT NULL
	DROP TABLE dbo.SystemForeignRelationshipDatabase
GO


CREATE TABLE dbo.SystemForeignRelationshipDatabase
(
		SystemForeignRelationshipDatabaseId		INT				NOT NULL
	,	ApplicationId							INT				NOT NULL
	,	Name									VARCHAR (50)	NOT NULL
	,	[Description]							VARCHAR (500)	NOT NULL
	,	SortOrder								INT				NOT NULL
);

