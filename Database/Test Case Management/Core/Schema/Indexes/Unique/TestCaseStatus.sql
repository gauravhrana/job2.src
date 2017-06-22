IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestCaseStatus]')
	AND		name	= N'UQ_TestCaseStatus_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestCaseStatus_ApplicationId_Name'
	ALTER	TABLE dbo.TestCaseStatus
	DROP	CONSTRAINT	UQ_TestCaseStatus_ApplicationId_Name
END
GO

ALTER TABLE dbo.TestCaseStatus
ADD CONSTRAINT UQ_TestCaseStatus_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId,
	Name
)
GO
