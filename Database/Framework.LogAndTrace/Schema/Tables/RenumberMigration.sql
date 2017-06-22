IF OBJECT_ID ('dbo.RenumberMigration') IS NOT NULL
	DROP TABLE dbo.RenumberMigration
GO

CREATE TABLE dbo.RenumberMigration
(
		RenumberMigrationId		INT				IDENTITY(1,1)	NOT NULL
	,	ApplicationId			INT								NOT NULL
	,	SystemEntityTypeId		INT								NOT NULL
	,	OriginalKey				INT								NOT NULL
	,	MigratedKey				INT								NOT NULL
	,	RecordDate				DATETIME						NOT NULL	
) 
 