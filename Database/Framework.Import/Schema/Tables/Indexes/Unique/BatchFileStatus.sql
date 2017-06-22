IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[BatchFileStatus]')
	AND		name	= N'UQ_BatchFileStatus_Name'
)
BEGIN
	PRINT	'Dropping UQ_BatchFileStatus_Name'
	ALTER	TABLE dbo.BatchFileStatus
	DROP	CONSTRAINT	UQ_BatchFileStatus_Name
END
GO

ALTER TABLE dbo.BatchFileStatus
ADD CONSTRAINT UQ_BatchFileStatus_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
