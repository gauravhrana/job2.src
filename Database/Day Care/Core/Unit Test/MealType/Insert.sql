/******************************************************************************
**		Name: MealType
*******************************************************************************/

EXEC dbo.MealType_Insert @MealTypeId = 41	,	@Name = 'Lunch'	    ,	@Description = 'FullPlate'		,	@SortOrder = 4
EXEC dbo.MealType_Insert @MealTypeId = 51	,	@Name = 'Dinner'	,	@Description = 'HalfPlate'	    ,   @SortOrder = 2
EXEC dbo.MealType_Insert @MealTypeId = 61	,	@Name = 'BreakFast'	,	@Description = 'FullPlate'	    ,	@SortOrder = 1

