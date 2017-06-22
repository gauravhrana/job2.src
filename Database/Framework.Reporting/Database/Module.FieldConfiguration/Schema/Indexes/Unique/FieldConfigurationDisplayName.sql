IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.FieldConfigurationDisplayName')
	AND		name	= N'UQ_FieldConfigurationDisplayName_ApplicationId_FieldConfigurationId_LanguageId_Value'
)
BEGIN
	PRINT	'Dropping UQ_FieldConfigurationDisplayName_ApplicationId_FieldConfigurationId_LanguageId_Value'
	ALTER TABLE dbo.FieldConfigurationDisplayName
		DROP CONSTRAINT	UQ_FieldConfigurationDisplayName_ApplicationId_FieldConfigurationId_LanguageId_Value
END
GO

ALTER TABLE dbo.FieldConfigurationDisplayName
	ADD CONSTRAINT UQ_FieldConfigurationDisplayName_ApplicationId_FieldConfigurationId_LanguageId_Value UNIQUE NONCLUSTERED
	(
			ApplicationId
		,	FieldConfigurationId
		,	LanguageId
		,	Value
	)
GO

IF EXISTS (SELECT * FROM sysindexes WHERE name = 'FC_DISPLAY_INDEX')
BEGIN
	PRINT 'Dropping index FC_DISPLAY_INDEX'
	DROP  INDEX  FC_DISPLAY_INDEX ON FieldConfigurationDisplayName
END
GO

PRINT 'Creating index FC_DISPLAY_INDEX'
GO

CREATE INDEX FC_DISPLAY_INDEX ON 
FieldConfigurationDisplayName(FieldConfigurationId)
