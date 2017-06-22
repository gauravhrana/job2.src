IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Financing')
	AND		name	= N'UQ_Financing_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Financing_ApplicationId_Name'
	ALTER	TABLE dbo.Financing
	DROP	CONSTRAINT	UQ_Financing_ApplicationId_Name
END
GO

ALTER TABLE dbo.Financing
ADD CONSTRAINT UQ_Financing_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
