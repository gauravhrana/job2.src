IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].RatingServices')
	AND		name	= N'UQ_RatingServices_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_RatingServices_ApplicationId_Name'
	ALTER	TABLE dbo.RatingServices
	DROP	CONSTRAINT	UQ_RatingServices_ApplicationId_Name
END
GO

ALTER TABLE dbo.RatingServices
ADD CONSTRAINT UQ_RatingServices_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
