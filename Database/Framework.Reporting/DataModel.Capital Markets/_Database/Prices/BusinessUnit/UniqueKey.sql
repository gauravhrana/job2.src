IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].BusinessUnit')
	AND		name	= N'UQ_BusinessUnit_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_BusinessUnit_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.BusinessUnit
	DROP	CONSTRAINT	UQ_BusinessUnit_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.BusinessUnit
ADD CONSTRAINT UQ_BusinessUnit_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
