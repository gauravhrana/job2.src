IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestSuiteXTestCase]')
	AND		name	= N'UQ_TestSuiteXTestCase_ApplicationId_TestSuiteId_TestCaseId_TestCasePriorityId_TestCaseStatusId'
)
BEGIN
	PRINT	'Dropping UQ_TestSuiteXTestCase_ApplicationId_TestSuiteId_TestCaseId_TestCasePriorityId_TestCaseStatusId'
	ALTER	TABLE dbo.TestSuiteXTestCase
	DROP	CONSTRAINT	UQ_TestSuiteXTestCase_ApplicationId_TestSuiteId_TestCaseId_TestCasePriorityId_TestCaseStatusId
END
GO

ALTER TABLE dbo.TestSuiteXTestCase
ADD CONSTRAINT UQ_TestSuiteXTestCase_ApplicationId_TestSuiteId_TestCaseId_TestCasePriorityId_TestCaseStatusId UNIQUE NONCLUSTERED
(
		ApplicationId
	,	TestSuiteId
	,	TestCaseId
	,	TestCasePriorityId
	,	TestCaseStatusId	
)
GO
