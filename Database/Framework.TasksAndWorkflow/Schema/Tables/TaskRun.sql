IF OBJECT_ID ('dbo.TaskRun') IS NOT NULL
	DROP TABLE dbo.TaskRun
GO

CREATE TABLE dbo.TaskRun 
(
		TaskRunId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
	,	TaskScheduleId		INT				NOT NULL	
	,	TaskEntityId		INT				NOT NULL
	,	BusinessDate		INT				NULL
	,	StartTime			DATETIME		NULL
	,	EndTime				DATETIME		NULL
	,	RunBy				VARCHAR(50)		NOT NULL   
    
);