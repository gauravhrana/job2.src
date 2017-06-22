IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CustodianAccount')
	AND		name	= N'UQ_CustodianAccount_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_CustodianAccount_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.CustodianAccount
	DROP	CONSTRAINT	UQ_CustodianAccount_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.CustodianAccount
ADD CONSTRAINT UQ_CustodianAccount_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
