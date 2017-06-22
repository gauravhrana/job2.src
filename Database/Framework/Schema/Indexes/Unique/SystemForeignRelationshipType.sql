IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[SystemForeignRelationshipType]')
	AND		name	= N'UQ_SystemForeignRelationshipType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SystemForeignRelationshipType_ApplicationId_Name'
	ALTER	TABLE dbo.SystemForeignRelationshipType 
	DROP	CONSTRAINT	UQ_SystemForeignRelationshipType_ApplicationId_Name
END
GO

ALTER TABLE dbo.SystemForeignRelationshipType 
ADD CONSTRAINT UQ_SystemForeignRelationshipType_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
