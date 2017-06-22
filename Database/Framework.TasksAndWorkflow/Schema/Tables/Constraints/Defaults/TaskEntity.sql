IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskEntity_Description'
)

ALTER TABLE dbo.TaskEntity
	ADD CONSTRAINT DF_TaskEntity_Description		DEFAULT		'' 		FOR Description
GO

IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskEntity_Active'
)

ALTER TABLE dbo.TaskEntity
	ADD CONSTRAINT DF_TaskEntity_Active				DEFAULT		1		FOR Active
GO

IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskEntity_SortOrder'
)

ALTER TABLE dbo.TaskEntity
	ADD CONSTRAINT DF_TaskEntity_SortOrder			DEFAULT		1000	FOR SortOrder
GO
