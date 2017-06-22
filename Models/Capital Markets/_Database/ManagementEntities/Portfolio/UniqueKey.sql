IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Portfolio')
	AND		name	= N'UQ_Portfolio_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Portfolio_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.Portfolio
	DROP	CONSTRAINT	UQ_Portfolio_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.Portfolio
ADD CONSTRAINT UQ_Portfolio_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
