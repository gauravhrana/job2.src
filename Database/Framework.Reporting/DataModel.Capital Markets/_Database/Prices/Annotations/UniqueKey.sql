IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Annotations')
	AND		name	= N'UQ_Annotations_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Annotations_ApplicationId_Name'
	ALTER	TABLE dbo.Annotations
	DROP	CONSTRAINT	UQ_Annotations_ApplicationId_Name
END
GO

ALTER TABLE dbo.Annotations
ADD CONSTRAINT UQ_Annotations_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
