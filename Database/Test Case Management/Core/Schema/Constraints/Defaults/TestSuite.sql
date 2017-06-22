	IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TestSuite_Description'
)

ALTER TABLE dbo.TestSuite
	ADD CONSTRAINT DF_TestSuite_Description		DEFAULT		'' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TestSuite_SortOrder'
)

ALTER TABLE dbo.TestSuite
	ADD CONSTRAINT DF_TestSuite_SortOrder			DEFAULT		1000	FOR SortOrder
GO
