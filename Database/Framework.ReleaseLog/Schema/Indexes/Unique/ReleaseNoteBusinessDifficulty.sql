IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReleaseNoteBusinessDifficulty]')
	AND		name	= N'UQ_ReleaseNoteBusinessDifficulty_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseNoteBusinessDifficulty_Name_ApplicationId'
	ALTER	TABLE dbo.ReleaseNoteBusinessDifficulty
	DROP	CONSTRAINT	UQ_ReleaseNoteBusinessDifficulty_Name_ApplicationId
END
GO

ALTER TABLE dbo.ReleaseNoteBusinessDifficulty
ADD CONSTRAINT UQ_ReleaseNoteBusinessDifficulty_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO
