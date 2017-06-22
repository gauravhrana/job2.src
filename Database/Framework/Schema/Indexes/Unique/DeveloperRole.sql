IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.DeveloperRole')
	AND		name	= N'UQ_DeveloperRole_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_DeveloperRole_Name_ApplicationId'
	ALTER TABLE dbo.DeveloperRole
		DROP CONSTRAINT	UQ_DeveloperRole_Name_ApplicationId
END
GO

ALTER TABLE dbo.DeveloperRole
	ADD CONSTRAINT UQ_DeveloperRole_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
