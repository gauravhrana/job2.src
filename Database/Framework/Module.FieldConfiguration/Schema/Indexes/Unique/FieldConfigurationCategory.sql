IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.FieldConfigurationCategory')
	AND		name	= N'UQ_FieldConfigurationCategory_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_FieldConfigurationCategory_Name_ApplicationId'
	ALTER TABLE dbo.FieldConfigurationCategory
		DROP CONSTRAINT	UQ_FieldConfigurationCategory_Name_ApplicationId
END
GO

ALTER TABLE dbo.FieldConfigurationCategory
	ADD CONSTRAINT UQ_FieldConfigurationCategory_Name_ApplicationId UNIQUE NONCLUSTERED
	(
			Name
		,	ApplicationId
	)
GO
