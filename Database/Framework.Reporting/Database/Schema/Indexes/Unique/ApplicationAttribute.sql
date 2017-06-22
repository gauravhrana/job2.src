IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ApplicationAttribute')
	AND		name	= N'UQ_ApplicationAttribute_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationAttribute_ApplicationId'
	ALTER TABLE dbo.ApplicationAttribute
		DROP CONSTRAINT	UQ_ApplicationAttribute_ApplicationId
END
GO

ALTER TABLE dbo.ApplicationAttribute
	ADD CONSTRAINT UQ_ApplicationAttribute_ApplicationId UNIQUE NONCLUSTERED
	(
			ApplicationId
	)
GO
