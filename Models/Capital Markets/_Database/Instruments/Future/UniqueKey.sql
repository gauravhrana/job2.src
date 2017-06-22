IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Future')
	AND		name	= N'UQ_Future_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Future_ApplicationId_Name'
	ALTER	TABLE dbo.Future
	DROP	CONSTRAINT	UQ_Future_ApplicationId_Name
END
GO

ALTER TABLE dbo.Future
ADD CONSTRAINT UQ_Future_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
