IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_DatabaseChangeLog_HostName'
)

ALTER TABLE dbo.DatabaseChangeLog
	ADD CONSTRAINT DF_DatabaseChangeLog_HostName		DEFAULT host_name() 		FOR HostName
GO

