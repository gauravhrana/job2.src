IF OBJECT_ID ('dbo.TaskNotes') IS NOT NULL
	DROP TABLE dbo.TaskNotes
GO

CREATE TABLE dbo.TaskNotes 
(
		TaskNotesId			INT				NOT NULL	
	,	ApplicationId		INT				NOT NULL
    ,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (100)	NOT NULL	
   	,	SortOrder			INT				NOT NULL
);

