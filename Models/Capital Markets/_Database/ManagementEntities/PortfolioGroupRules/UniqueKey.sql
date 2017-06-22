IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PortfolioGroupRules')
	AND		name	= N'UQ_PortfolioGroupRules_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PortfolioGroupRules_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.PortfolioGroupRules
	DROP	CONSTRAINT	UQ_PortfolioGroupRules_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.PortfolioGroupRules
ADD CONSTRAINT UQ_PortfolioGroupRules_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
