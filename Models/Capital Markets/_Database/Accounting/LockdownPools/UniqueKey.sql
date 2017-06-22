IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].LockdownPools')
	AND		name	= N'UQ_LockdownPools_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_LockdownPools_ApplicationId_Name'
	ALTER	TABLE dbo.LockdownPools
	DROP	CONSTRAINT	UQ_LockdownPools_ApplicationId_Name
END
GO

ALTER TABLE dbo.LockdownPools
ADD CONSTRAINT UQ_LockdownPools_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
