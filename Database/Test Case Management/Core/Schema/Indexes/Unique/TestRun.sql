IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TestRun]')
	AND		name	= N'UQ_TestRun_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TestRun_ApplicationId_Name'
	ALTER	TABLE dbo.TestRun
	DROP	CONSTRAINT	UQ_TestRun_ApplicationId_Name
END
GO

ALTER TABLE dbo.TestRun
ADD CONSTRAINT UQ_TestRun_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId,
	Name
)
GO
