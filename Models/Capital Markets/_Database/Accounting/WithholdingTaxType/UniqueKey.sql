IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].WithholdingTaxType')
	AND		name	= N'UQ_WithholdingTaxType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_WithholdingTaxType_ApplicationId_Name'
	ALTER	TABLE dbo.WithholdingTaxType
	DROP	CONSTRAINT	UQ_WithholdingTaxType_ApplicationId_Name
END
GO

ALTER TABLE dbo.WithholdingTaxType
ADD CONSTRAINT UQ_WithholdingTaxType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
