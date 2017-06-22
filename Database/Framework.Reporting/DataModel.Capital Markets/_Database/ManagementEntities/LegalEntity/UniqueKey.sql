IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].LegalEntity')
	AND		name	= N'UQ_LegalEntity_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_LegalEntity_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.LegalEntity
	DROP	CONSTRAINT	UQ_LegalEntity_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.LegalEntity
ADD CONSTRAINT UQ_LegalEntity_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
