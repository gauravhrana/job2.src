IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.Language')
	AND		name	= N'UQ_Language_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_Language_Name_ApplicationId'
	ALTER TABLE dbo.Language
		DROP CONSTRAINT	UQ_Language_Name_ApplicationId
END
GO

ALTER TABLE dbo.Language
	ADD CONSTRAINT UQ_Language_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
