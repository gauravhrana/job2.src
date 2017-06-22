IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.FieldConfigurationModeXApplicationUser')
	AND		name	= N'UQ_FCModeXAppRole_FCModeId_ApplicationUserId_FCModeAccessModeId_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_FCModeXAppRole_FCModeId_ApplicationUserId_FCModeAccessModeId_ApplicationId'
	ALTER TABLE dbo.FieldConfigurationModeXApplicationUser
		DROP CONSTRAINT	UQ_FCModeXAppRole_FCModeId_ApplicationUserId_FCModeAccessModeId_ApplicationId
END
GO

ALTER TABLE dbo.FieldConfigurationModeXApplicationUser
	ADD CONSTRAINT UQ_FCModeXAppRole_FCModeId_ApplicationUserId_FCModeAccessModeId_ApplicationId UNIQUE NONCLUSTERED
	(
			FieldConfigurationModeId
		,	ApplicationUserId
		,	FieldConfigurationModeAccessModeId
		,	ApplicationId
	)
GO
