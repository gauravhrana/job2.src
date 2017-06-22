IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.FieldConfigurationModeXApplicationRole')
	AND		name	= N'UQ_FCModeXAppRole_FCModeId_ApplicationRoleId_FCModeAccessModeId_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_FCModeXAppRole_FCModeId_ApplicationRoleId_FCModeAccessModeId_ApplicationId'
	ALTER TABLE dbo.FieldConfigurationModeXApplicationRole
		DROP CONSTRAINT	UQ_FCModeXAppRole_FCModeId_ApplicationRoleId_FCModeAccessModeId_ApplicationId
END
GO

ALTER TABLE dbo.FieldConfigurationModeXApplicationRole
	ADD CONSTRAINT UQ_FCModeXAppRole_FCModeId_ApplicationRoleId_FCModeAccessModeId_ApplicationId UNIQUE NONCLUSTERED
	(
			FieldConfigurationModeId
		,	ApplicationRoleId
		,	FieldConfigurationModeAccessModeId
		,	ApplicationId
	)
GO
