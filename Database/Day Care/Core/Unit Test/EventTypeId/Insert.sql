/******************************************************************************
**		Name: EventType
*******************************************************************************/

EXEC dbo.EventType_Insert @EventTypeId = 54	,	@Name = 'Drama'  	,	@Description = 'BigScreen'		,	@SortOrder = 3
EXEC dbo.EventType_Insert @EventTypeId = 35	,	@Name = 'Play'	    ,	@Description = 'SmallScreen'	,   @SortOrder = 9
EXEC dbo.EventType_Insert @EventTypeId = 65	,	@Name = 'Serial'	,	@Description = 'Television'	    ,	@SortOrder = 10

