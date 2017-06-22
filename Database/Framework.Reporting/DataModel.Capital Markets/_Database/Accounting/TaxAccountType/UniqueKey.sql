IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TaxAccountType')
	AND		name	= N'UQ_TaxAccountType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TaxAccountType_ApplicationId_Name'
	ALTER	TABLE dbo.TaxAccountType
	DROP	CONSTRAINT	UQ_TaxAccountType_ApplicationId_Name
END
GO

ALTER TABLE dbo.TaxAccountType
ADD CONSTRAINT UQ_TaxAccountType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
