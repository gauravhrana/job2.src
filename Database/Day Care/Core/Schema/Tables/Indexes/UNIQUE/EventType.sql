IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[EventType]')
	AND		name	= N'UQ_EventType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_EventType_ApplicationId_Name'
	ALTER	TABLE dbo.EventType
	DROP	CONSTRAINT	UQ_EventType_ApplicationId_Name
END
GO

ALTER TABLE dbo.EventType
ADD CONSTRAINT UQ_EventType_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
