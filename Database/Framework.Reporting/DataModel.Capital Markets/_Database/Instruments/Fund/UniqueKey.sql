IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Fund')
	AND		name	= N'UQ_Fund_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Fund_ApplicationId_Name'
	ALTER	TABLE dbo.Fund
	DROP	CONSTRAINT	UQ_Fund_ApplicationId_Name
END
GO

ALTER TABLE dbo.Fund
ADD CONSTRAINT UQ_Fund_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
