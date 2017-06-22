IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ApplicationMode')
	AND		name	= N'UQ_ApplicationMode_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationMode_Name_ApplicationId'
	ALTER TABLE dbo.ApplicationMode
		DROP CONSTRAINT	UQ_ApplicationMode_Name_ApplicationId
END
GO

ALTER TABLE dbo.ApplicationMode
	ADD CONSTRAINT UQ_ApplicationMode_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
