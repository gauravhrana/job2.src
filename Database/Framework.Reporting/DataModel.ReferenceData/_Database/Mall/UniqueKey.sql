IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Mall')
	AND		name	= N'UQ_Mall_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Mall_ApplicationId_Name'
	ALTER	TABLE dbo.Mall
	DROP	CONSTRAINT	UQ_Mall_ApplicationId_Name
END
GO

ALTER TABLE dbo.Mall
ADD CONSTRAINT UQ_Mall_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
