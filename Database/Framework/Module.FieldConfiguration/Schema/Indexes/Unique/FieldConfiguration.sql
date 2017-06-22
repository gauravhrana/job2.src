IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.FieldConfiguration')
	AND		name	= N'UQ_FieldConfiguration_ApplicationId_Name_SystemEntityTypeId_FCModeId'
)
BEGIN
	PRINT	'Dropping UQ_FieldConfiguration_ApplicationId_Name_SystemEntityTypeId_FCModeId'
	ALTER TABLE dbo.FieldConfiguration
		DROP CONSTRAINT	UQ_FieldConfiguration_ApplicationId_Name_SystemEntityTypeId_FCModeId
END
GO

ALTER TABLE dbo.FieldConfiguration
	ADD CONSTRAINT UQ_FieldConfiguration_ApplicationId_Name_SystemEntityTypeId_FCModeId  UNIQUE NONCLUSTERED
	(
				ApplicationId
			,	Name
			,	SystemEntityTypeId
			,	FieldConfigurationModeId 
	)
GO
