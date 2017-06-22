IF OBJECT_Id ('dbo.Activity') IS NOT NULL
   DROP TABLE dbo.Activity
   
GO

CREATE TABLE dbo.Activity
(
		ActivityId        INT NOT NULL
	,	ApplicationId     INT NOT NULL
	,	StudentId         INT NOT NULL
	,	ActivityTypeId    INT NOT NULL
	,	ActivitySubTypeId INT NOT NULL
)
GO

