IF OBJECT_Id ('dbo.Bathroom') IS NOT NULL
   DROP TABLE dbo.Bathroom
GO

CREATE TABLE dbo.Bathroom 
(
		BathroomId     INT          NOT NULL
	,	ApplicationId  INT          NOT NULL
	,	StudentId      INT          NOT NULL
	,	TimeIn         DATETIME     NOT NULL
	,	DiaperStatusId INT          NOT NULL
	,	DiaperCream    VARCHAR (50) NOT NULL
	,	PottyStatus    VARCHAR (50) NOT NULL
	,	TeacherId      INT          NOT NULL
	,	TeacherNotes   VARCHAR (50) NOT NULL
)
GO

