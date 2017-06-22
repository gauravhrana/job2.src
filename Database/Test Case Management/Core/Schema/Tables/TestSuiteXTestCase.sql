
IF OBJECT_ID ('dbo.TestSuiteXTestCase') IS NOT NULL
	DROP TABLE dbo.TestSuiteXTestCase
GO
CREATE TABLE dbo.TestSuiteXTestCase
(	TestSuiteXTestCaseId	INT NOT NULL,
	ApplicationId		INT NOT NULL,
	TestSuiteId			INT NOT NULL,
	TestCaseId			INT NOT NULL,
	TestCaseStatusId			INT				NOT NULL,	
	TestCasePriorityId			INT				NOT NULL	
) 

GO


