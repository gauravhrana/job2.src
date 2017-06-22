IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[Discount]')
	AND		name	= N'UQ_Discount_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Discount_ApplicationId_Name'
	ALTER	TABLE dbo.Discount
	DROP	CONSTRAINT	UQ_Discount_ApplicationId_Name
END
GO

ALTER TABLE dbo.Discount
ADD CONSTRAINT UQ_Discount_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
