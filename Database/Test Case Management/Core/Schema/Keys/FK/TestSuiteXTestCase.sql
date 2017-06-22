ALTER TABLE dbo.TestSuiteXTestCase
	ADD CONSTRAINT FK_TestSuiteXTestCase_TestCase FOREIGN KEY
	(
		TestCaseId
	)
	REFERENCES TestCase
	(
		TestCaseId
	)
GO

ALTER TABLE dbo.TestSuiteXTestCase
	ADD CONSTRAINT FK_TestSuiteXTestCase_TestSuite FOREIGN KEY
	(
		TestSuiteId
	)
	REFERENCES dbo.TestSuite
	(
		TestSuiteId
	)
GO