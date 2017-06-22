IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ApplicationRole')
	AND		name	= N'UQ_ApplicationRole_Name'
)
BEGIN
	PRINT	'Dropping UQ_AplicationRole_Name'
	ALTER TABLE dbo.ApplicationRole
		DROP CONSTRAINT	UQ_ApplicationRole_Name
END
GO

ALTER TABLE dbo.ApplicationRole
	ADD CONSTRAINT UQ_ApplicationRole_Name UNIQUE NONCLUSTERED
	(
		Name
	)
GO
