IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SecurityClass')
	AND		name	= N'UQ_SecurityClass_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SecurityClass_ApplicationId_Name'
	ALTER	TABLE dbo.SecurityClass
	DROP	CONSTRAINT	UQ_SecurityClass_ApplicationId_Name
END
GO

ALTER TABLE dbo.SecurityClass
ADD CONSTRAINT UQ_SecurityClass_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
