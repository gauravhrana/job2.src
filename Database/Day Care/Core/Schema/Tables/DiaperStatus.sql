IF OBJECT_Id ('dbo.DiaperStatus') IS NOT NULL
   DROP TABLE dbo.DiaperStatus
GO

CREATE TABLE dbo.DiaperStatus 
(
		DiaperStatusId INT           NOT NULL
	,	ApplicationId  INT           NOT NULL
	,	Name           VARCHAR (50)  NOT NULL
	,	Description    VARCHAR (500) NOT NULL
	,	SortOrder      INT           NOT NULL
)
GO

