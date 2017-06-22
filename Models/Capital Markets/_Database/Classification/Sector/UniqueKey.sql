IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Sector')
	AND		name	= N'UQ_Sector_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Sector_ApplicationId_Name'
	ALTER	TABLE dbo.Sector
	DROP	CONSTRAINT	UQ_Sector_ApplicationId_Name
END
GO

ALTER TABLE dbo.Sector
ADD CONSTRAINT UQ_Sector_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
