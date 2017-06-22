IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.SuperKey')
	AND		name	= N'UQ_SuperKey_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SuperKey_ApplicationId_Name'
	ALTER TABLE dbo.SuperKey
		DROP CONSTRAINT	UQ_SuperKey_ApplicationId_Name
END
GO

ALTER TABLE dbo.SuperKey
	ADD CONSTRAINT UQ_SuperKey_ApplicationId_Name UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	Name
	)
GO
