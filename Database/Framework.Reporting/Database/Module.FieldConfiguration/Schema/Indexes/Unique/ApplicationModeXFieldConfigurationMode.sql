IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.ApplicationModeXFieldConfigurationMode')
	AND		name	= N'UQ_ApplicationModeXFieldConfigurationMode_ApplicationModeId_FieldConfigurationModeId_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationModeXFieldConfigurationMode_ApplicationModeId_FieldConfigurationModeId_ApplicationId'
	ALTER TABLE dbo.ApplicationModeXFieldConfigurationMode
		DROP CONSTRAINT	UQ_ApplicationModeXFieldConfigurationMode_ApplicationModeId_FieldConfigurationModeId_ApplicationId
END
GO

ALTER TABLE dbo.ApplicationModeXFieldConfigurationMode
	ADD CONSTRAINT UQ_ApplicationModeXFieldConfigurationMode_ApplicationModeId_FieldConfigurationModeId_ApplicationId UNIQUE NONCLUSTERED
	(
			ApplicationModeId
		,	FieldConfigurationModeId
		,	ApplicationId
	)
GO
