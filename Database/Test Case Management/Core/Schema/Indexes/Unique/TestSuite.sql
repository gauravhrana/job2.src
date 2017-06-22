IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestSuite]')
	AND		name	= N'UQ_TestSuite_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestSuite_Name'
	ALTER	TABLE dbo.TestSuite
	DROP	CONSTRAINT	UQ_TestSuite_Name
END
GO

ALTER TABLE dbo.TestSuite
ADD CONSTRAINT UQ_TestSuite_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
