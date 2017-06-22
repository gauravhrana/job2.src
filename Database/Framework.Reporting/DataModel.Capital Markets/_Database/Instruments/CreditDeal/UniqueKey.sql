IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CreditDeal')
	AND		name	= N'UQ_CreditDeal_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_CreditDeal_ApplicationId_Name'
	ALTER	TABLE dbo.CreditDeal
	DROP	CONSTRAINT	UQ_CreditDeal_ApplicationId_Name
END
GO

ALTER TABLE dbo.CreditDeal
ADD CONSTRAINT UQ_CreditDeal_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
