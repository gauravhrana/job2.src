IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CompanyDealType')
	AND		name	= N'UQ_CompanyDealType_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_CompanyDealType_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.CompanyDealType
	DROP	CONSTRAINT	UQ_CompanyDealType_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.CompanyDealType
ADD CONSTRAINT UQ_CompanyDealType_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
