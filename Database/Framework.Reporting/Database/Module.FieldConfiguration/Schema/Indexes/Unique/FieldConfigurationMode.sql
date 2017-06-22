IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.FieldConfigurationMode')
	AND		name	= N'UQ_FieldConfigurationMode_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_FieldConfigurationMode_Name_ApplicationId'
	ALTER TABLE dbo.FieldConfigurationMode
		DROP CONSTRAINT	UQ_FieldConfigurationMode_Name_ApplicationId
END
GO

ALTER TABLE dbo.FieldConfigurationMode
	ADD CONSTRAINT UQ_FieldConfigurationMode_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO

IF EXISTS (SELECT * FROM sysindexes WHERE name = 'FC_FCMODE_INDEX')
BEGIN
	PRINT 'Dropping index FC_FCMODE_INDEX'
	DROP  INDEX  FC_FCMODE_INDEX ON FieldConfigurationMode
END
GO

PRINT 'Creating index FC_FCMODE_INDEX'
GO

CREATE INDEX FC_FCMODE_INDEX ON 
FieldConfigurationMode(FieldConfigurationModeId)