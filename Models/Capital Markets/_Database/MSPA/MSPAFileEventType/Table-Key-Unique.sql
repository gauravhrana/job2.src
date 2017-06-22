IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].MSPAFileEventType')
	AND		name	= N'UQ_MSPAFileEventType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_MSPAFileEventType_ApplicationId_Name'
	ALTER	TABLE dbo.MSPAFileEventType
	DROP	CONSTRAINT	UQ_MSPAFileEventType_ApplicationId_Name
END
GO

ALTER TABLE dbo.MSPAFileEventType
ADD CONSTRAINT UQ_MSPAFileEventType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
