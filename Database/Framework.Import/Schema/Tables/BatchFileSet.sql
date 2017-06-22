IF OBJECT_ID ('dbo.BatchFileSet') IS NOT NULL
	DROP TABLE dbo.BatchFileSet
GO

CREATE TABLE dbo.BatchFileSet 
(
		BatchFileSetId		INT				NOT NULL
	,	ApplicationId		INT				NOT NULL	
    ,	Name				VARCHAR (50)	NOT NULL	
    ,	Description			VARCHAR (50)	NOT NULL	
    ,	CreatedDate			DATETIME		NOT NULL
	,	CreatedByPersonId	INT				NOT NULL
);

