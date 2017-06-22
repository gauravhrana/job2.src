IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CreditDefaultSwapIndex')
	AND		name	= N'UQ_CreditDefaultSwapIndex_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_CreditDefaultSwapIndex_ApplicationId_Name'
	ALTER	TABLE dbo.CreditDefaultSwapIndex
	DROP	CONSTRAINT	UQ_CreditDefaultSwapIndex_ApplicationId_Name
END
GO

ALTER TABLE dbo.CreditDefaultSwapIndex
ADD CONSTRAINT UQ_CreditDefaultSwapIndex_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
