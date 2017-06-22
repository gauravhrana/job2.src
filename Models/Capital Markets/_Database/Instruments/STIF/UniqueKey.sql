IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].STIF')
	AND		name	= N'UQ_STIF_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_STIF_ApplicationId_Name'
	ALTER	TABLE dbo.STIF
	DROP	CONSTRAINT	UQ_STIF_ApplicationId_Name
END
GO

ALTER TABLE dbo.STIF
ADD CONSTRAINT UQ_STIF_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
