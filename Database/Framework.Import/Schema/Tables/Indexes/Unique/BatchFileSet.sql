IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[BatchFileSet]')
	AND		name	= N'UQ_BatchFileSet_Name'
)
BEGIN
	PRINT	'Dropping UQ_BatchFileSet_Name'
	ALTER	TABLE dbo.BatchFileSet
	DROP	CONSTRAINT	UQ_BatchFileSet_Name
END
GO

ALTER TABLE dbo.BatchFileSet
ADD CONSTRAINT UQ_BatchFileSet_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
