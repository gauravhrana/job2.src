IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].TaxStatus')
	AND		name	= N'UQ_TaxStatus_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TaxStatus_ApplicationId_Name'
	ALTER	TABLE dbo.TaxStatus
	DROP	CONSTRAINT	UQ_TaxStatus_ApplicationId_Name
END
GO

ALTER TABLE dbo.TaxStatus
ADD CONSTRAINT UQ_TaxStatus_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
