IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Tuition')
	AND		name	= N'UQ_Tuition_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Tuition_ApplicationId_Name'
	ALTER	TABLE dbo.Tuition
	DROP	CONSTRAINT	UQ_Tuition_ApplicationId_Name
END
GO

ALTER TABLE dbo.Tuition
ADD CONSTRAINT UQ_Tuition_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
