IF OBJECT_ID ('dbo.TaskScheduleType') IS NOT NULL
	DROP TABLE dbo.TaskScheduleType
GO

CREATE TABLE dbo.TaskScheduleType 
(
		TaskScheduleTypeId	INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
    ,	Description			VARCHAR (50)	NOT NULL	
    ,	Active				INT				NOT NULL
	,	SortOrder			INT				NOT NULL
);

