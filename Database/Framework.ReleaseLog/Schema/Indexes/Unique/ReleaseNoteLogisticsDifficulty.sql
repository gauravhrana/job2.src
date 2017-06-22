IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReleaseNoteLogisticsDifficulty]')
	AND		name	= N'UQ_ReleaseNoteLogisticsDifficulty_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseNoteLogisticsDifficulty_Name_ApplicationId'
	ALTER	TABLE dbo.ReleaseNoteLogisticsDifficulty
	DROP	CONSTRAINT	UQ_ReleaseNoteLogisticsDifficulty_Name_ApplicationId
END
GO

ALTER TABLE dbo.ReleaseNoteLogisticsDifficulty
ADD CONSTRAINT UQ_ReleaseNoteLogisticsDifficulty_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO
