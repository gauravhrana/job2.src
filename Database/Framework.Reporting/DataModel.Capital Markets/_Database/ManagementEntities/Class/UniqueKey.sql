IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Class')
	AND		name	= N'UQ_Class_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Class_ApplicationId_Name'
	ALTER	TABLE dbo.Class
	DROP	CONSTRAINT	UQ_Class_ApplicationId_Name
END
GO

ALTER TABLE dbo.Class
ADD CONSTRAINT UQ_Class_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
