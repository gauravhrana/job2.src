IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].DiaperStatus')
	AND		name	= N'UQ_DiaperStatus_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_DiaperStatus_ApplicationId_Name'
	ALTER	TABLE dbo.DiaperStatus
	DROP	CONSTRAINT	UQ_DiaperStatus_ApplicationId_Name
END
GO

ALTER TABLE dbo.DiaperStatus
ADD CONSTRAINT UQ_DiaperStatus_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
