IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].ActivitySubType')
	AND		name	= N'UQ_ActivitySubType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ActivitySubType_ApplicationId_Name'
	ALTER	TABLE dbo.ActivitySubType
	DROP	CONSTRAINT	UQ_ActivitySubType_ApplicationId_Name
END
GO

ALTER TABLE dbo.ActivitySubType
ADD CONSTRAINT UQ_ActivitySubType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO
