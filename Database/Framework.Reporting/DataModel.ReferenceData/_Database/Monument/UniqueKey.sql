IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Monument')
	AND		name	= N'UQ_Monument_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Monument_ApplicationId_Name'
	ALTER	TABLE dbo.Monument
	DROP	CONSTRAINT	UQ_Monument_ApplicationId_Name
END
GO

ALTER TABLE dbo.Monument
ADD CONSTRAINT UQ_Monument_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
