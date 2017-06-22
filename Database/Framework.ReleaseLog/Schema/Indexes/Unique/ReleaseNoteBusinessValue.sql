IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReleaseNoteBusinessValue]')
	AND		name	= N'UQ_ReleaseNoteBusinessValue_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseNoteBusinessValue_Name_ApplicationId'
	ALTER	TABLE dbo.ReleaseNoteBusinessValue
	DROP	CONSTRAINT	UQ_ReleaseNoteBusinessValue_Name_ApplicationId
END
GO

ALTER TABLE dbo.ReleaseNoteBusinessValue
ADD CONSTRAINT UQ_ReleaseNoteBusinessValue_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO
