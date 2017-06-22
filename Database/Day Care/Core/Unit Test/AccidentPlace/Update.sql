/******************************************************************************
**		Name: AccidentPlace
*******************************************************************************/

EXEC dbo.AccidentPlace_Update @AccidentPlaceId = 10	,	@Name = 'Tom'	    ,	@Description = 'London'	     ,	@SortOrder = 13
EXEC dbo.AccidentPlace_Update @AccidentPlaceId = 11	,	@Name = 'Rome'      ,	@Description = 'Paris'		 ,  @SortOrder = 14
EXEC dbo.AccidentPlace_Update @AccidentPlaceId = 12 ,	@Name = 'Saif'	    ,	@Description = 'America'	 ,	@SortOrder = 15

