IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].FreezePoints')
	AND		name	= N'UQ_FreezePoints_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_FreezePoints_ApplicationId_Name'
	ALTER	TABLE dbo.FreezePoints
	DROP	CONSTRAINT	UQ_FreezePoints_ApplicationId_Name
END
GO

ALTER TABLE dbo.FreezePoints
ADD CONSTRAINT UQ_FreezePoints_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
