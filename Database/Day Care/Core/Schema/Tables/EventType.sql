IF OBJECT_Id ('dbo.EventType') IS NOT NULL
   DROP TABLE dbo.EventType
GO

CREATE TABLE dbo.EventType 
(
		EventTypeId		INT           NOT NULL
	,	ApplicationId	INT           NOT NULL
	,	Name			VARCHAR (50)  NOT NULL
	,	Description		VARCHAR (500) NOT NULL
	,	SortOrder		INT           NOT NULL
)
GO

