IF OBJECT_Id ('dbo.AccidentReport') IS NOT NULL
   DROP TABLE dbo.AccidentReport
GO

CREATE TABLE dbo.AccidentReport 
(
		AccidentReportId INT           NOT NULL
	,	ApplicationId	 INT           NOT NULL
	,	StudentId        INT           NOT NULL
	,	Date             DATETIME      NOT NULL
	,	AccidentPlaceId  INT           NOT NULL
	,	TeacherId        INT           NOT NULL
	,	Description     VARCHAR (500) NOT NULL
	,	Remedy           VARCHAR (200) NOT NULL
	,	SignoffParent    BIT           NOT NULL
	,	SignoffTeacher   BIT           NOT NULL
	,	SignoffAdmin     BIT           NOT NULL
)
GO
