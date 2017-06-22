IF OBJECT_ID ('dbo.TaskSchedule') IS NOT NULL
	DROP TABLE dbo.TaskSchedule
GO

CREATE TABLE dbo.TaskSchedule 
(
		TaskScheduleId		INT				IDENTITY	NOT NULL
	,	ApplicationId		INT							NOT NULL	
    ,	TaskScheduleTypeId	INT							NOT NULL	
    ,	TaskEntityId		INT							NOT NULL
);

