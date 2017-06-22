/******************************************************************************
**		Name: Meal
*******************************************************************************/

EXEC dbo.Meal_Update @MealId = 411	,	@StudentId = 14	  ,	@Date  = '12/21/2011'		,	@MealType = 123
EXEC dbo.Meal_Update @MealId = 511	,	@StudentId = 15	  ,	@Date =  '12/22/2011'	    ,   @MealType = 234
EXEC dbo.Meal_Update @MealId = 611	,	@StudentId = 16   ,	@Date =  '12/23/2011'	    ,	@MealType = 345

