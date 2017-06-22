IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.SystemEntityType')
	AND		Name	= N'UQ_SystemEntityType_EntityName'
)
BEGIN
	PRINT	'Dropping UQ_SystemEntityType_EntityName'
	ALTER TABLE dbo.SystemEntityType
		DROP CONSTRAINT	UQ_SystemEntityType_EntityName
END
GO

ALTER TABLE dbo.SystemEntityType
	ADD CONSTRAINT UQ_SystemEntityType_EntityName UNIQUE NONCLUSTERED
	(
		EntityName
	)
GO
