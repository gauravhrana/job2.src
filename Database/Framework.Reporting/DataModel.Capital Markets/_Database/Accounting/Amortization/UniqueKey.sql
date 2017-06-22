IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Amortization')
	AND		name	= N'UQ_Amortization_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Amortization_ApplicationId_Name'
	ALTER	TABLE dbo.Amortization
	DROP	CONSTRAINT	UQ_Amortization_ApplicationId_Name
END
GO

ALTER TABLE dbo.Amortization
ADD CONSTRAINT UQ_Amortization_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
