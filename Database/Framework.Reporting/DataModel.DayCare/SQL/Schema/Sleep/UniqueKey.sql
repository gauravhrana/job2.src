IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Sleep')
	AND		name	= N'UQ_Sleep_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Sleep_ApplicationId_Name'
	ALTER	TABLE dbo.Sleep
	DROP	CONSTRAINT	UQ_Sleep_ApplicationId_Name
END
GO

ALTER TABLE dbo.Sleep
ADD CONSTRAINT UQ_Sleep_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
