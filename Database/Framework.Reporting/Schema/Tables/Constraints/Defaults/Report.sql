IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Report_Description'
)

ALTER TABLE dbo.Report
	ADD CONSTRAINT DF_Report_Description		DEFAULT '' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_Report_SortOrder'
)

ALTER TABLE dbo.Report
	ADD CONSTRAINT DF_Report_SortOrder		DEFAULT 1000		FOR SortOrder
GO
