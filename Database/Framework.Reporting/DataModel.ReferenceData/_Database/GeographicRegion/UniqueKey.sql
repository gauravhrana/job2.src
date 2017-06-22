IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].GeographicRegion')
	AND		name	= N'UQ_GeographicRegion_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_GeographicRegion_ApplicationId_Name'
	ALTER	TABLE dbo.GeographicRegion
	DROP	CONSTRAINT	UQ_GeographicRegion_ApplicationId_Name
END
GO

ALTER TABLE dbo.GeographicRegion
ADD CONSTRAINT UQ_GeographicRegion_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
