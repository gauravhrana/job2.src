--IF NOT EXISTS
--(
--	SELECT	name
--	FROM	sys.objects
--	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
--	AND		name			= 'DF_Trace_Description'
--)

ALTER TABLE dbo.Trace
	ADD CONSTRAINT DF_Trace_Description		DEFAULT '' 		FOR Description
GO


--IF NOT EXISTS
--(
--	SELECT	name
--	FROM	sys.objects
--	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
--	AND		name			= 'DF_Trace_SortOrder'
--)

ALTER TABLE dbo.Trace
	ADD CONSTRAINT DF_Trace_SortOrder		DEFAULT 1000		FOR SortOrder
GO
