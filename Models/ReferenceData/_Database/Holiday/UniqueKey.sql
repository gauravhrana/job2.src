IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Holiday')
	AND		name	= N'UQ_Holiday_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Holiday_ApplicationId_Name'
	ALTER	TABLE dbo.Holiday
	DROP	CONSTRAINT	UQ_Holiday_ApplicationId_Name
END
GO

ALTER TABLE dbo.Holiday
ADD CONSTRAINT UQ_Holiday_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
