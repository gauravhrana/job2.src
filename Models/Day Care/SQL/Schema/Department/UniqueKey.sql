IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Department')
	AND		name	= N'UQ_Department_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Department_ApplicationId_Name'
	ALTER	TABLE dbo.Department
	DROP	CONSTRAINT	UQ_Department_ApplicationId_Name
END
GO

ALTER TABLE dbo.Department
ADD CONSTRAINT UQ_Department_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
