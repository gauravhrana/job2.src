IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Custodian')
	AND		name	= N'UQ_Custodian_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Custodian_ApplicationId_Name'
	ALTER	TABLE dbo.Custodian
	DROP	CONSTRAINT	UQ_Custodian_ApplicationId_Name
END
GO

ALTER TABLE dbo.Custodian
ADD CONSTRAINT UQ_Custodian_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
