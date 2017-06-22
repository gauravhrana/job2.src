IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SubBusinessUnit')
	AND		name	= N'UQ_SubBusinessUnit_ApplicationId_FundId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SubBusinessUnit_ApplicationId_FundId_Name'
	ALTER	TABLE dbo.SubBusinessUnit
	DROP	CONSTRAINT	UQ_SubBusinessUnit_ApplicationId_FundId_Name
END
GO

ALTER TABLE dbo.SubBusinessUnit
ADD CONSTRAINT UQ_SubBusinessUnit_ApplicationId_FundId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, FundId, Name
)
GO
