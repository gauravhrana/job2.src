IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].ChartOfAccounts')
	AND		name	= N'UQ_ChartOfAccounts_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ChartOfAccounts_ApplicationId_Name'
	ALTER	TABLE dbo.ChartOfAccounts
	DROP	CONSTRAINT	UQ_ChartOfAccounts_ApplicationId_Name
END
GO

ALTER TABLE dbo.ChartOfAccounts
ADD CONSTRAINT UQ_ChartOfAccounts_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
