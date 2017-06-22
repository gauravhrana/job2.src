IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SecurityType')
	AND		name	= N'UQ_SecurityType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SecurityType_ApplicationId_Name'
	ALTER	TABLE dbo.SecurityType
	DROP	CONSTRAINT	UQ_SecurityType_ApplicationId_Name
END
GO

ALTER TABLE dbo.SecurityType
ADD CONSTRAINT UQ_SecurityType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
