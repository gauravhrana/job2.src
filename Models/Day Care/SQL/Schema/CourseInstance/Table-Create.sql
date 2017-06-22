IF OBJECT_ID ('dbo.CourseInstance') IS NOT NULL
	DROP TABLE dbo.CourseInstance
GO

CREATE TABLE dbo.CourseInstance
(
		CourseInstanceId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	CourseId				INT		NOT NULL
	,	DepartmentId				INT		NOT NULL
	,	TeacherId				INT		NOT NULL
	,	StartTime				DATETIME		NULL
	,	EndTime				DATETIME		NULL
)
GO
