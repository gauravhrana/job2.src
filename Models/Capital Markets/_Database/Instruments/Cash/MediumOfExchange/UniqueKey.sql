IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].MediumOfExchange')
	AND		name	= N'UQ_MediumOfExchange_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_MediumOfExchange_ApplicationId_Name'
	ALTER	TABLE dbo.MediumOfExchange
	DROP	CONSTRAINT	UQ_MediumOfExchange_ApplicationId_Name
END
GO

ALTER TABLE dbo.MediumOfExchange
ADD CONSTRAINT UQ_MediumOfExchange_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
