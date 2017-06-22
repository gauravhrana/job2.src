	IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TestCase_Description'
)

ALTER TABLE dbo.TestCase
	ADD CONSTRAINT DF_TestCase_Description		DEFAULT		'' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TestCase_SortOrder'
)

ALTER TABLE dbo.TestCase
	ADD CONSTRAINT DF_TestCase_SortOrder			DEFAULT		1000	FOR SortOrder
GO
