/******************************************************************************
**		Name: NeedItem
*******************************************************************************/

EXEC dbo.NeedItem_Insert @NeedItemId = 24	,	@Name = 'Vegetable'	   ,	@Description = 'ToEat'		,	@SortOrder = 3
EXEC dbo.NeedItem_Insert @NeedItemId = 25	,	@Name = 'Oil'	       ,	@Description = 'ToCook'	    ,   @SortOrder = 5
EXEC dbo.NeedItem_Insert @NeedItemId = 26	,	@Name = 'Utensil'	   ,	@Description = 'ToMake'	    ,	@SortOrder = 9

