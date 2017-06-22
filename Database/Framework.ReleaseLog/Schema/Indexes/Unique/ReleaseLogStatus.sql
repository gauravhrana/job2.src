IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReleaseLogStatus]')
	AND		name	= N'UQ_ReleaseLogStatus_Name'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseLogStatus_Name'
	ALTER	TABLE dbo.ReleaseLogStatus
	DROP	CONSTRAINT	UQ_ReleaseLogStatus_Name
END
GO

ALTER TABLE dbo.ReleaseLogStatus
ADD CONSTRAINT UQ_ReleaseLogStatus_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
