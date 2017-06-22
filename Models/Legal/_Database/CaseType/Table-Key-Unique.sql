IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CaseType')
	AND		name	= N'UQ_CaseType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_CaseType_ApplicationId_Name'
	ALTER	TABLE dbo.CaseType
	DROP	CONSTRAINT	UQ_CaseType_ApplicationId_Name
END
GO

ALTER TABLE dbo.CaseType
ADD CONSTRAINT UQ_CaseType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
