IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReleaseFeature]')
	AND		name	= N'UQ_ReleaseFeature_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseFeature_Name_ApplicationId'
	ALTER	TABLE dbo.ReleaseFeature
	DROP	CONSTRAINT	UQ_ReleaseFeature_Name_ApplicationId
END
GO

ALTER TABLE dbo.ReleaseFeature
ADD CONSTRAINT UQ_ReleaseFeature_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO
