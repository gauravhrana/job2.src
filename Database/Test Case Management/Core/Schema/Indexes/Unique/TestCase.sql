IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestCase]')
	AND		name	= N'UQ_TestCase_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestCase_Name'
	ALTER	TABLE dbo.TestCase
	DROP	CONSTRAINT	UQ_TestCase_Name
END
GO

ALTER TABLE dbo.TestCase
ADD CONSTRAINT UQ_TestCase_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
