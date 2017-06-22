IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Rating')
	AND		name	= N'UQ_Rating_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Rating_ApplicationId_Name'
	ALTER	TABLE dbo.Rating
	DROP	CONSTRAINT	UQ_Rating_ApplicationId_Name
END
GO

ALTER TABLE dbo.Rating
ADD CONSTRAINT UQ_Rating_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
