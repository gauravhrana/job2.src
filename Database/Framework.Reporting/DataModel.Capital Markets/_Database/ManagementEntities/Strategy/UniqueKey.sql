IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Strategy')
	AND		name	= N'UQ_Strategy_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Strategy_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.Strategy
	DROP	CONSTRAINT	UQ_Strategy_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.Strategy
ADD CONSTRAINT UQ_Strategy_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
