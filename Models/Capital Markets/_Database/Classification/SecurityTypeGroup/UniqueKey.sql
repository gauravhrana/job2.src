IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SecurityTypeGroup')
	AND		name	= N'UQ_SecurityTypeGroup_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SecurityTypeGroup_ApplicationId_Name'
	ALTER	TABLE dbo.SecurityTypeGroup
	DROP	CONSTRAINT	UQ_SecurityTypeGroup_ApplicationId_Name
END
GO

ALTER TABLE dbo.SecurityTypeGroup
ADD CONSTRAINT UQ_SecurityTypeGroup_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, Name
)
GO
