IF OBJECT_Id ('dbo.SickReport') IS NOT NULL
PRINT 'Dropping Table SickReport'
   DROP TABLE dbo.SickReport
GO
PRINT 'Creating Table SickReport'
CREATE TABLE dbo.SickReport
(
		SickReportId       INT           NOT NULL
    ,	StudentId          INT           NOT NULL
    ,	TypeOfSickness     VARCHAR (50)  NOT NULL
    ,	AmountOfSickness   VARCHAR (50)  NOT NULL
    ,	FreqOfSickness     VARCHAR (50)  NOT NULL
    ,	TeacherSickNote    VARCHAR (100) NOT NULL
    ,	ReturnToSchoolDate DATETIME      NOT NULL
)
GO

