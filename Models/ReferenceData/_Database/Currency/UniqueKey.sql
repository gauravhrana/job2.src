IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Currency')
	AND		name	= N'UQ_Currency_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Currency_ApplicationId_Name'
	ALTER	TABLE dbo.Currency
	DROP	CONSTRAINT	UQ_Currency_ApplicationId_Name
END
GO

ALTER TABLE dbo.Currency
ADD CONSTRAINT UQ_Currency_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
