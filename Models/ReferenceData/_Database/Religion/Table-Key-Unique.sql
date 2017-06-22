IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Religion')
	AND		name	= N'UQ_Religion_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Religion_ApplicationId_Name'
	ALTER	TABLE dbo.Religion
	DROP	CONSTRAINT	UQ_Religion_ApplicationId_Name
END
GO

ALTER TABLE dbo.Religion
ADD CONSTRAINT UQ_Religion_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
