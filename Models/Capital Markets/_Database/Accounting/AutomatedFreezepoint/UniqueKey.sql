IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AutomatedFreezepoint')
	AND		name	= N'UQ_AutomatedFreezepoint_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AutomatedFreezepoint_ApplicationId_Name'
	ALTER	TABLE dbo.AutomatedFreezepoint
	DROP	CONSTRAINT	UQ_AutomatedFreezepoint_ApplicationId_Name
END
GO

ALTER TABLE dbo.AutomatedFreezepoint
ADD CONSTRAINT UQ_AutomatedFreezepoint_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
