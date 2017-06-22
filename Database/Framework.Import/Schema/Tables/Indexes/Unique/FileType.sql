IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[FileType]')
	AND		name	= N'UQ_FileType_Name'
)
BEGIN
	PRINT	'Dropping UQ_FileType_Name'
	ALTER	TABLE dbo.FileType
	DROP	CONSTRAINT	UQ_FileType_Name
END
GO

ALTER TABLE dbo.FileType
ADD CONSTRAINT UQ_FileType_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
