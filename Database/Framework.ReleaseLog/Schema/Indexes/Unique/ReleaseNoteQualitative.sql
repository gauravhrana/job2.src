IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReleaseNoteQualitative]')
	AND		name	= N'UQ_ReleaseNoteQualitative_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseNoteQualitative_Name_ApplicationId'
	ALTER	TABLE dbo.ReleaseNoteQualitative
	DROP	CONSTRAINT	UQ_ReleaseNoteQualitative_Name_ApplicationId
END
GO

ALTER TABLE dbo.ReleaseNoteQualitative
ADD CONSTRAINT UQ_ReleaseNoteQualitative_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO
