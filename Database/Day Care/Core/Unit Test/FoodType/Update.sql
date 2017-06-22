/******************************************************************************
**		Name: FoodType
*******************************************************************************/

EXEC dbo.FoodType_Update @FoodTypeId = 110	,	@Name = 'Chowmein'	    ,	@Description = 'FastFood'	 ,	@SortOrder = 23
EXEC dbo.FoodType_Update @FoodTypeId = 111	,	@Name = 'VegSalad'      ,	@Description = 'Healty'		 ,  @SortOrder = 24
EXEC dbo.FoodType_Update @FoodTypeId = 112 ,	@Name = 'FriedRice'	    ,	@Description = 'Tasty'	     ,	@SortOrder = 25

