IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SecurityXInvestmentIdentifier')
	AND		name	= N'UQ_SecurityXInvestmentIdentifier_ApplicationId_SecurityId'
)
BEGIN
	PRINT	'Dropping UQ_SecurityXInvestmentIdentifier_ApplicationId_SecurityId'
	ALTER	TABLE dbo.SecurityXInvestmentIdentifier
	DROP	CONSTRAINT	UQ_SecurityXInvestmentIdentifier_ApplicationId_SecurityId
END
GO

ALTER TABLE dbo.SecurityXInvestmentIdentifier
ADD CONSTRAINT UQ_SecurityXInvestmentIdentifier_ApplicationId_SecurityId UNIQUE NONCLUSTERED
(
	ApplicationId, SecurityId
)
GO
