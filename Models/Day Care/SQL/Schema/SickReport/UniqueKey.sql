IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SickReport')
	AND		name	= N'UQ_SickReport_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SickReport_ApplicationId_Name'
	ALTER	TABLE dbo.SickReport
	DROP	CONSTRAINT	UQ_SickReport_ApplicationId_Name
END
GO

ALTER TABLE dbo.SickReport
ADD CONSTRAINT UQ_SickReport_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
