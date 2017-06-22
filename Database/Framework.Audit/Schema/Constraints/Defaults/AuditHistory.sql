
--IF NOT EXISTS
--(
--	SELECT	name
--	FROM	sys.objects
--	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
--	AND		name			= 'DF_AuditHistory_TraceId'
--)

ALTER TABLE dbo.AuditHistory
	ADD CONSTRAINT DF_AuditHistory_TraceId		DEFAULT -1		FOR TraceId
GO