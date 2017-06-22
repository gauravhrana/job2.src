IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Trader')
	AND		name	= N'UQ_Trader_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Trader_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.Trader
	DROP	CONSTRAINT	UQ_Trader_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.Trader
ADD CONSTRAINT UQ_Trader_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
