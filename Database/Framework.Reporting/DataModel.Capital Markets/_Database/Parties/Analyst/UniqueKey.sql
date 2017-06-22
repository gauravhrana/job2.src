IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Analyst')
	AND		name	= N'UQ_Analyst_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Analyst_ApplicationId_Name'
	ALTER	TABLE dbo.Analyst
	DROP	CONSTRAINT	UQ_Analyst_ApplicationId_Name
END
GO

ALTER TABLE dbo.Analyst
ADD CONSTRAINT UQ_Analyst_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
