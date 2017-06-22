IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Gender')
	AND		name	= N'UQ_Gender_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Gender_ApplicationId_Name'
	ALTER	TABLE dbo.Gender
	DROP	CONSTRAINT	UQ_Gender_ApplicationId_Name
END
GO

ALTER TABLE dbo.Gender
ADD CONSTRAINT UQ_Gender_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
