IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.AEFLModeCategoryXApplicationModeXAEFLMode')
	AND		name	= N'UQ_AEFLModeCategoryXApplicationModeXAEFLMode_AEFLModeCategoryId_ApplicationModeId_ApplicationEntityFieldLabelModeId'
)
BEGIN
	PRINT	'Dropping UQ_AEFLModeCategoryXApplicationModeXAEFLMode_AEFLModeCategoryId_ApplicationModeId_ApplicationEntityFieldLabelModeId'
	ALTER TABLE dbo.AEFLModeCategoryXApplicationModeXAEFLMode
		DROP CONSTRAINT	UQ_AEFLModeCategoryXApplicationModeXAEFLMode_AEFLModeCategoryId_ApplicationModeId_ApplicationEntityFieldLabelModeId
END
GO

ALTER TABLE dbo.AEFLModeCategoryXApplicationModeXAEFLMode
	ADD CONSTRAINT UQ_AEFLModeCategoryXApplicationModeXAEFLMode_AEFLModeCategoryId_ApplicationModeId_ApplicationEntityFieldLabelModeId UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	AEFLModeCategoryId
		,	ApplicationModeId
		,	ApplicationEntityFieldLabelModeId
	)
GO
	