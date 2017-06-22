/******************************************************************************
**		Name: FoodType
*******************************************************************************/

EXEC dbo.FoodType_Insert @FoodTypeId = 41	,	@Name = 'Pizza'	    ,	@Description = 'FastFood'	,	@SortOrder = 1
EXEC dbo.FoodType_Insert @FoodTypeId = 51	,	@Name = 'Burger'	,	@Description = 'FastFood'	,   @SortOrder = 2
EXEC dbo.FoodType_Insert @FoodTypeId = 61	,	@Name = 'Fruit'	    ,	@Description = 'Healty'	    ,	@SortOrder = 3

