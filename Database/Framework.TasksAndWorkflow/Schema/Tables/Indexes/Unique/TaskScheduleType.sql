IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TaskScheduleType]')
	AND		name	= N'UQ_TaskScheduleType_Name'
)
BEGIN
	PRINT	'Dropping UQ_TaskScheduleType_Name'
	ALTER	TABLE dbo.TaskScheduleType
	DROP	CONSTRAINT	UQ_TaskScheduleType_Name
END
GO

ALTER TABLE dbo.TaskScheduleType
ADD CONSTRAINT UQ_TaskScheduleType_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
