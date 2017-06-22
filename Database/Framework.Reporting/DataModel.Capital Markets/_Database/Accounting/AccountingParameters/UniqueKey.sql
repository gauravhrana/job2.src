IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AccountingParameters')
	AND		name	= N'UQ_AccountingParameters_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccountingParameters_ApplicationId_Name'
	ALTER	TABLE dbo.AccountingParameters
	DROP	CONSTRAINT	UQ_AccountingParameters_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccountingParameters
ADD CONSTRAINT UQ_AccountingParameters_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
