IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].FinancialAccountClass')
	AND		name	= N'UQ_FinancialAccountClass_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_FinancialAccountClass_ApplicationId_Name'
	ALTER	TABLE dbo.FinancialAccountClass
	DROP	CONSTRAINT	UQ_FinancialAccountClass_ApplicationId_Name
END
GO

ALTER TABLE dbo.FinancialAccountClass
ADD CONSTRAINT UQ_FinancialAccountClass_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
