IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.Module')
	AND		name	= N'UQ_Module_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_Module_Name_ApplicationId'
	ALTER TABLE dbo.Module
		DROP CONSTRAINT	UQ_Module_Name_ApplicationId
END
GO

ALTER TABLE dbo.Module
	ADD CONSTRAINT UQ_Module_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
