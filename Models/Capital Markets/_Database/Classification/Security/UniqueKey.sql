IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Security')
	AND		name	= N'UQ_Security_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Security_ApplicationId_Name'
	ALTER	TABLE dbo.Security
	DROP	CONSTRAINT	UQ_Security_ApplicationId_Name
END
GO

ALTER TABLE dbo.Security
ADD CONSTRAINT UQ_Security_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
