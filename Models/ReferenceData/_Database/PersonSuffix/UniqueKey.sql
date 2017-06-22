IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PersonSuffix')
	AND		name	= N'UQ_PersonSuffix_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PersonSuffix_ApplicationId_Name'
	ALTER	TABLE dbo.PersonSuffix
	DROP	CONSTRAINT	UQ_PersonSuffix_ApplicationId_Name
END
GO

ALTER TABLE dbo.PersonSuffix
ADD CONSTRAINT UQ_PersonSuffix_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
