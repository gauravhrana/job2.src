IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AccountingView')
	AND		name	= N'UQ_AccountingView_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccountingView_ApplicationId_Name'
	ALTER	TABLE dbo.AccountingView
	DROP	CONSTRAINT	UQ_AccountingView_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccountingView
ADD CONSTRAINT UQ_AccountingView_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
