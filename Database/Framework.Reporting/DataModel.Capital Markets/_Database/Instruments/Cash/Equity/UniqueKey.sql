IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Equity')
	AND		name	= N'UQ_Equity_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Equity_ApplicationId_Name'
	ALTER	TABLE dbo.Equity
	DROP	CONSTRAINT	UQ_Equity_ApplicationId_Name
END
GO

ALTER TABLE dbo.Equity
ADD CONSTRAINT UQ_Equity_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
