IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_RenumberMigration_HostName'
)

ALTER TABLE dbo.RenumberMigration
	ADD CONSTRAINT DF_RenumberMigration_HostName		DEFAULT host_name() 		FOR HostName
GO

