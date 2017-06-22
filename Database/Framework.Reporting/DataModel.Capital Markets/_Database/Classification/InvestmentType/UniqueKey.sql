IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].InvestmentType')
	AND		name	= N'UQ_InvestmentType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_InvestmentType_ApplicationId_Name'
	ALTER	TABLE dbo.InvestmentType
	DROP	CONSTRAINT	UQ_InvestmentType_ApplicationId_Name
END
GO

ALTER TABLE dbo.InvestmentType
ADD CONSTRAINT UQ_InvestmentType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
