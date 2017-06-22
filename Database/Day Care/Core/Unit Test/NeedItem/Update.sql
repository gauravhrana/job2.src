/******************************************************************************
**		Name: NeedItem
*******************************************************************************/

EXEC dbo.NeedItem_Update @NeedItemId = 214	,	@Name = 'Plates'	   ,	@Description = 'ToServer'	,	@SortOrder = 34
EXEC dbo.NeedItem_Update @NeedItemId = 215	,	@Name = 'Vegetable'	   ,	@Description = 'ToEat'	    ,   @SortOrder = 45
EXEC dbo.NeedItem_Update @NeedItemId = 216	,	@Name = 'Oil'	       ,	@Description = 'ToCook'	    ,	@SortOrder = 49
  
