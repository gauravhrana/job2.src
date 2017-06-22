	IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskNotes_Description'
)

ALTER TABLE dbo.TaskNotes
	ADD CONSTRAINT DF_TaskNotes_Description		DEFAULT		'' 		FOR Description
GO


IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskNotes_SortOrder'
)

ALTER TABLE dbo.TaskNotes
	ADD CONSTRAINT DF_TaskNotes_SortOrder			DEFAULT		1000	FOR SortOrder
GO
