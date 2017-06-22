IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PerformanceKey')
	AND		name	= N'UQ_PerformanceKey_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PerformanceKey_ApplicationId_Name'
	ALTER	TABLE dbo.PerformanceKey
	DROP	CONSTRAINT	UQ_PerformanceKey_ApplicationId_Name
END
GO

ALTER TABLE dbo.PerformanceKey
ADD CONSTRAINT UQ_PerformanceKey_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
