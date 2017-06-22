IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ReleaseNoteDeveloperValue]')
	AND		name	= N'UQ_ReleaseNoteDeveloperValue_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ReleaseNoteDeveloperValue_Name_ApplicationId'
	ALTER	TABLE dbo.ReleaseNoteDeveloperValue
	DROP	CONSTRAINT	UQ_ReleaseNoteDeveloperValue_Name_ApplicationId
END
GO

ALTER TABLE dbo.ReleaseNoteDeveloperValue
ADD CONSTRAINT UQ_ReleaseNoteDeveloperValue_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO
