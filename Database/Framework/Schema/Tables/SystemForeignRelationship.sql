IF OBJECT_ID ('dbo.SystemForeignRelationship') IS NOT NULL
	DROP TABLE dbo.SystemForeignRelationship
GO

CREATE TABLE dbo.SystemForeignRelationship
(
		SystemForeignRelationshipId     INT NOT NULL,
		ApplicationId                   INT NOT NULL,
		PrimaryDatabaseId               INT NOT NULL,
		PrimaryEntityId                 INT NOT NULL,
		ForeignDatabaseId               INT NOT NULL,
		ForeignEntityId                 INT NOT NULL,
		FieldName                       VARCHAR (50) NULL,
		SystemForeignRelationshipTypeId INT NOT NULL,
)


