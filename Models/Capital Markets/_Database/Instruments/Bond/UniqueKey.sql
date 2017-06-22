IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Bond')
	AND		name	= N'UQ_Bond_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Bond_ApplicationId_Name'
	ALTER	TABLE dbo.Bond
	DROP	CONSTRAINT	UQ_Bond_ApplicationId_Name
END
GO

ALTER TABLE dbo.Bond
ADD CONSTRAINT UQ_Bond_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
