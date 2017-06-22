IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReleaseNoteTechnicalDifficulty]')
	AND		name	= N'UQ_ReleaseNoteTechnicalDifficulty_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseNoteTechnicalDifficulty_Name_ApplicationId'
	ALTER	TABLE dbo.ReleaseNoteTechnicalDifficulty
	DROP	CONSTRAINT	UQ_ReleaseNoteTechnicalDifficulty_Name_ApplicationId
END
GO

ALTER TABLE dbo.ReleaseNoteTechnicalDifficulty
ADD CONSTRAINT UQ_ReleaseNoteTechnicalDifficulty_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO
