IF OBJECT_ID ('dbo.TimeZone') IS NOT NULL
	DROP TABLE dbo.TimeZone
GO

CREATE TABLE dbo.TimeZone 
(
		TimeZoneId			INT				NOT NULL
	,   ApplicationId		INT				NOT NULL 	
	,	Name				VARCHAR (50)	NOT NULL	
	,	Description			VARCHAR (50)	NOT NULL	
	,	SortOrder			INT				NOT NULL
	,	TimeDifference		DECIMAL(4,2)	NOT NULL
);

