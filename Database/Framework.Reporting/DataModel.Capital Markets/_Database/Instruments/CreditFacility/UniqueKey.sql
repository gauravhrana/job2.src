IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CreditFacility')
	AND		name	= N'UQ_CreditFacility_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_CreditFacility_ApplicationId_Name'
	ALTER	TABLE dbo.CreditFacility
	DROP	CONSTRAINT	UQ_CreditFacility_ApplicationId_Name
END
GO

ALTER TABLE dbo.CreditFacility
ADD CONSTRAINT UQ_CreditFacility_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
