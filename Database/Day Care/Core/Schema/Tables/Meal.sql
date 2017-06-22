IF OBJECT_Id ('dbo.Meal') IS NOT NULL
   DROP TABLE dbo.Meal
GO

CREATE TABLE dbo.Meal
(
		MealId			INT      NOT NULL
	,	ApplicationId	INT		 NOT NULL
	,	StudentId		INT      NOT NULL
	,	Date			DATETIME NOT NULL
	,	MealTypeId		INT      NOT NULL
)
GO

