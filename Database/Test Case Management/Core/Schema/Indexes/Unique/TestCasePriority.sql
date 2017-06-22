IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestCasePriority]')
	AND		name	= N'UQ_TestCasePriority_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestCasePriority_ApplicationId_Name'
	ALTER	TABLE dbo.TestCasePriority
	DROP	CONSTRAINT	UQ_TestCasePriority_ApplicationId_Name
END
GO

ALTER TABLE dbo.TestCasePriority
ADD CONSTRAINT UQ_TestCasePriority_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId,
	Name
)
GO
