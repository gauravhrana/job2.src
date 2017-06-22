IF OBJECT_ID ('dbo.ClassInstance') IS NOT NULL
	DROP TABLE dbo.ClassInstance
GO

CREATE TABLE dbo.ClassInstance
(
		ClassInstanceId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	CourseId				INT		NOT NULL
	,	DepartmentId				INT		NOT NULL
	,	TeacherId				INT		NOT NULL
	,	StartTime				DATETIME		NULL
	,	EndTime				DATETIME		NULL
)
GO
