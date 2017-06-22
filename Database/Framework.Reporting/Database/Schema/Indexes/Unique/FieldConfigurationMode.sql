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
