IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Manager')
	AND		name	= N'UQ_Manager_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Manager_ApplicationId_Name'
	ALTER	TABLE dbo.Manager
	DROP	CONSTRAINT	UQ_Manager_ApplicationId_Name
END
GO

ALTER TABLE dbo.Manager
ADD CONSTRAINT UQ_Manager_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
