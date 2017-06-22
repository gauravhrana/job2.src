IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Country')
	AND		name	= N'UQ_Country_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Country_ApplicationId_Name'
	ALTER	TABLE dbo.Country
	DROP	CONSTRAINT	UQ_Country_ApplicationId_Name
END
GO

ALTER TABLE dbo.Country
ADD CONSTRAINT UQ_Country_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
