IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].StrategyGroup')
	AND		name	= N'UQ_StrategyGroup_ApplicationId_FundId'
)
BEGIN
	PRINT	'Dropping UQ_StrategyGroup_ApplicationId_FundId'
	ALTER	TABLE dbo.StrategyGroup
	DROP	CONSTRAINT	UQ_StrategyGroup_ApplicationId_FundId
END
GO

ALTER TABLE dbo.StrategyGroup
ADD CONSTRAINT UQ_StrategyGroup_ApplicationId_FundId UNIQUE NONCLUSTERED
(
	ApplicationId, FundId
)
GO
