IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Ethnicity')
	AND		name	= N'UQ_Ethnicity_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Ethnicity_ApplicationId_Name'
	ALTER	TABLE dbo.Ethnicity
	DROP	CONSTRAINT	UQ_Ethnicity_ApplicationId_Name
END
GO

ALTER TABLE dbo.Ethnicity
ADD CONSTRAINT UQ_Ethnicity_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
