IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.FieldConfigurationModeAccessMode')
	AND		name	= N'UQ_FieldConfigurationModeAccessMode_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_FieldConfigurationModeAccessMode_Name_ApplicationId'
	ALTER TABLE dbo.FieldConfigurationModeAccessMode
		DROP CONSTRAINT	UQ_FieldConfigurationModeAccessMode_Name_ApplicationId
END
GO

ALTER TABLE dbo.FieldConfigurationModeAccessMode
	ADD CONSTRAINT UQ_FieldConfigurationModeAccessMode_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO

IF EXISTS (SELECT * FROM sysindexes WHERE name = 'FC_FCACCESSMODE_INDEX')
BEGIN
	PRINT 'Dropping index FC_FCACCESSMODE_INDEX'
	DROP  INDEX  FC_FCACCESSMODE_INDEX ON FieldConfigurationModeAccessMode
END
GO

PRINT 'Creating index FC_FCACCESSMODE_INDEX'
GO

CREATE INDEX FC_FCACCESSMODE_INDEX ON
FieldConfigurationModeAccessMode (FieldConfigurationModeAccessModeId)
