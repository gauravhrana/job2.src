/******************************************************************************
**		Name: MealType
*******************************************************************************/

EXEC dbo.MealType_Update @MealTypeId = 411	,	@Name = 'BrakFast'	    ,	@Description = 'HalfPlate'		,	@SortOrder = 41
EXEC dbo.MealType_Update @MealTypeId = 511	,	@Name = 'Dinner'	    ,	@Description = 'HalfPlate'	    ,   @SortOrder = 21
EXEC dbo.MealType_Update @MealTypeId = 611	,	@Name = 'Lunch'      	,	@Description = 'FullPlate'	    ,	@SortOrder = 11

