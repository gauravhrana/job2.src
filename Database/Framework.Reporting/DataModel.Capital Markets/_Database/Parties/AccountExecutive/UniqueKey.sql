IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AccountExecutive')
	AND		name	= N'UQ_AccountExecutive_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccountExecutive_ApplicationId_Name'
	ALTER	TABLE dbo.AccountExecutive
	DROP	CONSTRAINT	UQ_AccountExecutive_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccountExecutive
ADD CONSTRAINT UQ_AccountExecutive_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
