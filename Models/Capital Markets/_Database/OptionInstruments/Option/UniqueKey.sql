IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Option')
	AND		name	= N'UQ_Option_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Option_ApplicationId_Name'
	ALTER	TABLE dbo.Option
	DROP	CONSTRAINT	UQ_Option_ApplicationId_Name
END
GO

ALTER TABLE dbo.Option
ADD CONSTRAINT UQ_Option_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
