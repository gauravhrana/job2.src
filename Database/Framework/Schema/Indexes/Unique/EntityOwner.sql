IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.EntityOwner')
	AND		name	= N'UQ_EntityOwner_ApplicationId_EntityId_DeveloperRoleId'
)
BEGIN
	PRINT	'Dropping UQ_EntityOwner_ApplicationId_EntityId_DeveloperRoleId'
	ALTER TABLE dbo.EntityOwner
		DROP CONSTRAINT	UQ_EntityOwner_ApplicationId_EntityId_DeveloperRoleId
END
GO

ALTER TABLE dbo.EntityOwner
	ADD CONSTRAINT UQ_EntityOwner_ApplicationId_EntityId_DeveloperRoleId UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	EntityId
		,	DeveloperRoleId
	)
GO
