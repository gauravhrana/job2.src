IF OBJECT_ID ('dbo.SystemForeignRelationshipType') IS NOT NULL
	DROP TABLE dbo.SystemForeignRelationshipType
GO


CREATE TABLE dbo.SystemForeignRelationshipType
(
		SystemForeignRelationshipTypeId	  INT				NOT NULL
	,	ApplicationId					  INT				NOT NULL
	,	Name							  VARCHAR (50)		NOT NULL
	,	[Description]				      VARCHAR (500)		NOT NULL
	,	SortOrder					      INT				NOT NULL
);

