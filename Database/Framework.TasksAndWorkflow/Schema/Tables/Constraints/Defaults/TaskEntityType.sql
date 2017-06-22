IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskEntityType_Description'
)

ALTER TABLE dbo.TaskEntityType
	ADD CONSTRAINT DF_TaskEntityType_Description		DEFAULT		'' 		FOR Description
GO

IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskEntityType_Active'
)

ALTER TABLE dbo.TaskEntityType
	ADD CONSTRAINT DF_TaskEntityType_Active				DEFAULT		1		FOR Active
GO

IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskEntityType_SortOrder'
)

ALTER TABLE dbo.TaskEntityType
	ADD CONSTRAINT DF_TaskEntityType_SortOrder			DEFAULT		1000	FOR SortOrder
GO
