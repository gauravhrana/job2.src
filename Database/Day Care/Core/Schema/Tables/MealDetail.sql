IF OBJECT_Id ('dbo.MealDetail') IS NOT NULL
PRINT 'Dropping Table MealDetail'
   DROP TABLE dbo.MealDetail
GO
PRINT 'Creating Table MealDetail'
CREATE TABLE dbo.MealDetail
(
		MealDetailId	INT   NOT NULL
	,	ApplicationId	INT	  NOT NULL
    ,	MealId			INT   NOT NULL
    ,	FoodTypeId		INT   NOT NULL
    ,	AmtFinished		FLOAT NOT NULL
)
GO

