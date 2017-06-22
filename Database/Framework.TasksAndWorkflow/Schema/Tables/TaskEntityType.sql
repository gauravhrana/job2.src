IF OBJECT_ID ('dbo.TaskEntityType') IS NOT NULL
	DROP TABLE dbo.TaskEntityType
GO

CREATE TABLE dbo.TaskEntityType 
(
		TaskEntityTypeId	INT				NOT NULL	
	,   ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
    ,	Description			VARCHAR (50)	NOT NULL	
    ,	Active				INT				NOT NULL
	,	SortOrder			INT				NOT NULL
);

