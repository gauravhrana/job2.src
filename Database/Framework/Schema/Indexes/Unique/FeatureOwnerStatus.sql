IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.FeatureOwnerStatus')
	AND		name	= N'UQ_FeatureOwnerStatus_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_FeatureOwnerStatus_Name_ApplicationId'
	ALTER TABLE dbo.FeatureOwnerStatus
		DROP CONSTRAINT	UQ_FeatureOwnerStatus_Name_ApplicationId
END
GO

ALTER TABLE dbo.FeatureOwnerStatus
	ADD CONSTRAINT UQ_FeatureOwnerStatus_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
