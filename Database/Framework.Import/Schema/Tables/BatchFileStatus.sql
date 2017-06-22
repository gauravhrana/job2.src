IF OBJECT_ID ('dbo.BatchFileStatus') IS NOT NULL
	DROP TABLE dbo.BatchFileStatus
GO

CREATE TABLE dbo.BatchFileStatus 
(
		BatchFileStatusId   INT          NOT NULL
	,	ApplicationId		INT			 NOT NULL	
    ,	Name				VARCHAR (50) NOT NULL	
    ,	Description			VARCHAR (50) NOT NULL	
    ,	SortOrder			INT          NOT NULL
);

