IF OBJECT_ID ('dbo.TestSuiteXTestCaseArchive') IS NOT NULL
	DROP TABLE dbo.TestSuiteXTestCaseArchive
GO

CREATE TABLE dbo.TestSuiteXTestCaseArchive
(
		TestSuiteXTestCaseArchiveId		INT				IDENTITY(1,1)	NOT NULL 
	,	TestSuiteXTestCaseId				INT								NOT NULL	
	,   ApplicationId							INT								NOT NULL 		
	,	TestSuiteId			INT NOT NULL
	,	TestCaseId			INT NOT NULL
	,	TestCaseStatusId			INT				NOT NULL	
	,	TestCasePriorityId			INT				NOT NULL	
	,	TestSuite					VARCHAR(50)						NOT NULL	
	,	TestCase							VARCHAR(50)						NOT NULL	
	,	TestCaseStatus						VARCHAR(50)						NOT NULL	
	,	TestCasePriority					VARCHAR(50)						NOT NULL	
	,	RecordDate								DECIMAL(15,0)					NOT NULL
	,	KnowledgeDate							DECIMAL(15,0)					NOT NULL
);

