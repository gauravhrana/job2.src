IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ModuleOwner')
	AND		name	= N'UQ_ModuleOwner_ApplicationId_ModuleId_DeveloperRoleId'
)
BEGIN
	PRINT	'Dropping UQ_ModuleOwner_ApplicationId_ModuleId_DeveloperRoleId'
	ALTER TABLE dbo.ModuleOwner
		DROP CONSTRAINT	UQ_ModuleOwner_ApplicationId_ModuleId_DeveloperRoleId
END
GO

ALTER TABLE dbo.ModuleOwner
	ADD CONSTRAINT UQ_ModuleOwner_ApplicationId_ModuleId_DeveloperRoleId UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	ModuleId
		,	DeveloperRoleId
	)
GO
