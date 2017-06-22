IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TWRSet')
	AND		name	= N'UQ_TWRSet_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TWRSet_ApplicationId_Name'
	ALTER	TABLE dbo.TWRSet
	DROP	CONSTRAINT	UQ_TWRSet_ApplicationId_Name
END
GO

ALTER TABLE dbo.TWRSet
ADD CONSTRAINT UQ_TWRSet_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
