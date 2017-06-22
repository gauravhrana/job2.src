IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].RaceType')
	AND		name	= N'UQ_RaceType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_RaceType_ApplicationId_Name'
	ALTER	TABLE dbo.RaceType
	DROP	CONSTRAINT	UQ_RaceType_ApplicationId_Name
END
GO

ALTER TABLE dbo.RaceType
ADD CONSTRAINT UQ_RaceType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
