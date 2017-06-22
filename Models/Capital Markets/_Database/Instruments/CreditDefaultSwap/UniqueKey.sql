IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CreditDefaultSwap')
	AND		name	= N'UQ_CreditDefaultSwap_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_CreditDefaultSwap_ApplicationId_Name'
	ALTER	TABLE dbo.CreditDefaultSwap
	DROP	CONSTRAINT	UQ_CreditDefaultSwap_ApplicationId_Name
END
GO

ALTER TABLE dbo.CreditDefaultSwap
ADD CONSTRAINT UQ_CreditDefaultSwap_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
