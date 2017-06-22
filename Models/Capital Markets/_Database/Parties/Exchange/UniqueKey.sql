IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Exchange')
	AND		name	= N'UQ_Exchange_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Exchange_ApplicationId_Name'
	ALTER	TABLE dbo.Exchange
	DROP	CONSTRAINT	UQ_Exchange_ApplicationId_Name
END
GO

ALTER TABLE dbo.Exchange
ADD CONSTRAINT UQ_Exchange_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
