IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].PortfolioType')
	AND		name	= N'UQ_PortfolioType_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_PortfolioType_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.PortfolioType
	DROP	CONSTRAINT	UQ_PortfolioType_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.PortfolioType
ADD CONSTRAINT UQ_PortfolioType_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
