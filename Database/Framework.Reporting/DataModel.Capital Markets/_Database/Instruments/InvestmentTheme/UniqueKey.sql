IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].InvestmentTheme')
	AND		name	= N'UQ_InvestmentTheme_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_InvestmentTheme_ApplicationId_Name'
	ALTER	TABLE dbo.InvestmentTheme
	DROP	CONSTRAINT	UQ_InvestmentTheme_ApplicationId_Name
END
GO

ALTER TABLE dbo.InvestmentTheme
ADD CONSTRAINT UQ_InvestmentTheme_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
