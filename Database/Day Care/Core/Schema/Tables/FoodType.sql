IF OBJECT_Id ('dbo.FoodType') IS NOT NULL
   DROP TABLE dbo.FoodType
GO

CREATE TABLE dbo.FoodType 
(
		FoodTypeId		INT           NOT NULL
	,	ApplicationId   INT           NOT NULL
	,	Name			VARCHAR (50)  NOT NULL
	,	Description		VARCHAR (500) NOT NULL
	,	SortOrder		INT           NOT NULL
)
GO

