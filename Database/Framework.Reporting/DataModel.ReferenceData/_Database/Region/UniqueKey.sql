IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Region')
	AND		name	= N'UQ_Region_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Region_ApplicationId_Name'
	ALTER	TABLE dbo.Region
	DROP	CONSTRAINT	UQ_Region_ApplicationId_Name
END
GO

ALTER TABLE dbo.Region
ADD CONSTRAINT UQ_Region_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
