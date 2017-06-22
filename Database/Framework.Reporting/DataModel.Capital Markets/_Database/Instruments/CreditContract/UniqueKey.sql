IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CreditContract')
	AND		name	= N'UQ_CreditContract_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_CreditContract_ApplicationId_Name'
	ALTER	TABLE dbo.CreditContract
	DROP	CONSTRAINT	UQ_CreditContract_ApplicationId_Name
END
GO

ALTER TABLE dbo.CreditContract
ADD CONSTRAINT UQ_CreditContract_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
