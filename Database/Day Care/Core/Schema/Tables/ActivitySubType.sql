IF OBJECT_Id ('dbo.ActivitySubType') IS NOT NULL
   DROP TABLE dbo.ActivitySubType
GO

CREATE TABLE dbo.ActivitySubType 
(
		ActivitySubTypeId  INT           NOT NULL
	,	ApplicationId	   INT           NULL
	,	ActivityTypeId     INT           NULL
	,	Name               VARCHAR (50)  NOT NULL
	,	Description        VARCHAR (500) NOT NULL
	,	SortOrder          INT           NOT NULL
)
GO


