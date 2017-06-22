IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Cash')
	AND		name	= N'UQ_Cash_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Cash_ApplicationId_Name'
	ALTER	TABLE dbo.Cash
	DROP	CONSTRAINT	UQ_Cash_ApplicationId_Name
END
GO

ALTER TABLE dbo.Cash
ADD CONSTRAINT UQ_Cash_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
