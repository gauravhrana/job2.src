IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].RecordType')
	AND		name	= N'UQ_RecordType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_RecordType_ApplicationId_Name'
	ALTER	TABLE dbo.RecordType
	DROP	CONSTRAINT	UQ_RecordType_ApplicationId_Name
END
GO

ALTER TABLE dbo.RecordType
ADD CONSTRAINT UQ_RecordType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
