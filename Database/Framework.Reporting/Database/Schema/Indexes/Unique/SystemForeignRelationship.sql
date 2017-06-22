IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.SystemForeignRelationship')
	AND		name	= N'UQ_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_Name_ApplicationId'
	ALTER TABLE dbo.SystemForeignRelationship
		DROP CONSTRAINT	UQ_Name_ApplicationId
END
GO

ALTER TABLE dbo.SystemForeignRelationship
	ADD CONSTRAINT UQ_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	PrimaryDatabaseId
		,   PrimaryEntityId
		, 	ForeignDatabaseId
		, 	ForeignEntityId
		, 	FieldName
		, 	SystemForeignRelationshipTypeId
	)
GO

