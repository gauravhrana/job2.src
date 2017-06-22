IF OBJECT_ID ('dbo.TaskEntity') IS NOT NULL
	DROP TABLE dbo.TaskEntity
GO

CREATE TABLE dbo.TaskEntity 
(
		TaskEntityId		INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	TaskEntityTypeId	INT				NOT NULL	
    ,	Description			VARCHAR (50)	NOT NULL	
    ,	Active				INT				NOT NULL
	,	SortOrder			INT				NOT NULL
);

