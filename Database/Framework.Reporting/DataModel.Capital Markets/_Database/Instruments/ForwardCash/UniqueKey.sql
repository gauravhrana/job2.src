IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].ForwardCash')
	AND		name	= N'UQ_ForwardCash_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ForwardCash_ApplicationId_Name'
	ALTER	TABLE dbo.ForwardCash
	DROP	CONSTRAINT	UQ_ForwardCash_ApplicationId_Name
END
GO

ALTER TABLE dbo.ForwardCash
ADD CONSTRAINT UQ_ForwardCash_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
