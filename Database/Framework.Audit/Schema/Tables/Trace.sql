--IF OBJECT_ID ('dbo.Trace') IS NOT NULL
--	DROP TABLE dbo.Trace
--GO

CREATE TABLE dbo.Trace
(
		TraceId				INT				IDENTITY(1,1)	NOT NULL
	,   ApplicationId		INT								NOT NULL 	
	,	Name				VARCHAR (50)					NOT NULL	
	,	Description			VARCHAR (50)					NOT NULL	
	,	SortOrder			INT								NOT NULL
);

