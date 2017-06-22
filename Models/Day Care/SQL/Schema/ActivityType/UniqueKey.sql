IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].ActivityType')
	AND		name	= N'UQ_ActivityType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ActivityType_ApplicationId_Name'
	ALTER	TABLE dbo.ActivityType
	DROP	CONSTRAINT	UQ_ActivityType_ApplicationId_Name
END
GO

ALTER TABLE dbo.ActivityType
ADD CONSTRAINT UQ_ActivityType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
