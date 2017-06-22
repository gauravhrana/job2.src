IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[SystemForeignRelationshipDatabase]')
	AND		name	= N'UQ_SystemForeignRelationshipDatabase_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SystemForeignRelationshipDatabase_ApplicationId_Name'
	ALTER	TABLE dbo.SystemForeignRelationshipDatabase 
	DROP	CONSTRAINT	UQ_SystemForeignRelationshipDatabase_ApplicationId_Name
END
GO

ALTER TABLE dbo.SystemForeignRelationshipDatabase 
ADD CONSTRAINT UQ_SystemForeignRelationshipDatabase_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
