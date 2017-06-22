IF OBJECT_Id ('dbo.ActivityType') IS NOT NULL
   DROP TABLE dbo.ActivityType
GO

CREATE TABLE dbo.ActivityType 

(
		ActivityTypeId	 INT           NOT NULL
	,	ApplicationId	 INT           NOT NULL
	,	Name			 VARCHAR (50)  NOT NULL
	,	Description      VARCHAR (500) NOT NULL
	,	SortOrder		 INT           NOT NULL
)
GO

