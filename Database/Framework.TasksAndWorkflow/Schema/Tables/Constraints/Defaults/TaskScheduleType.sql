IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskScheduleType_Description'
)

ALTER TABLE dbo.TaskScheduleType
	ADD CONSTRAINT DF_TaskScheduleType_Description		DEFAULT		'' 		FOR Description
GO

IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskScheduleType_Active'
)

ALTER TABLE dbo.TaskScheduleType
	ADD CONSTRAINT DF_TaskScheduleType_Active				DEFAULT		1		FOR Active
GO

IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_TaskScheduleType_SortOrder'
)

ALTER TABLE dbo.TaskScheduleType
	ADD CONSTRAINT DF_TaskScheduleType_SortOrder			DEFAULT		1000	FOR SortOrder
GO
