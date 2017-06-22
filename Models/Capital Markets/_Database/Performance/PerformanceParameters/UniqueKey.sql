IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PerformanceParameters')
	AND		name	= N'UQ_PerformanceParameters_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PerformanceParameters_ApplicationId_Name'
	ALTER	TABLE dbo.PerformanceParameters
	DROP	CONSTRAINT	UQ_PerformanceParameters_ApplicationId_Name
END
GO

ALTER TABLE dbo.PerformanceParameters
ADD CONSTRAINT UQ_PerformanceParameters_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
