IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestCaseOwner]')
	AND		name	= N'UQ_TestCaseOwner_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestCaseOwner_ApplicationId_Name'
	ALTER	TABLE dbo.TestCaseOwner
	DROP	CONSTRAINT	UQ_TestCaseOwner_ApplicationId_Name
END
GO

ALTER TABLE dbo.TestCaseOwner
ADD CONSTRAINT UQ_TestCaseOwner_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId,
	Name
)
GO
