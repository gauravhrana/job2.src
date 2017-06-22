IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Issuer')
	AND		name	= N'UQ_Issuer_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Issuer_ApplicationId_Name'
	ALTER	TABLE dbo.Issuer
	DROP	CONSTRAINT	UQ_Issuer_ApplicationId_Name
END
GO

ALTER TABLE dbo.Issuer
ADD CONSTRAINT UQ_Issuer_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
