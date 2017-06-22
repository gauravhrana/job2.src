/******************************************************************************
**		Name: Comment
*******************************************************************************/

EXEC dbo.Comment_Insert @CommentId = 117	,	@StudentId = 120	 ,	@Date = '12/21/2011'	,	@EventTypeId = 123    , @Comment = 'GoodStudent'
EXEC dbo.Comment_Insert @CommentId = 118	,	@StudentId = 121     ,	@Date = '12/22/2011'	,   @EventTypeId = 124    , @Comment = 'WorkHard'
EXEC dbo.Comment_Insert @CommentId = 119    ,	@StudentId = 122     ,	@Date = '12/23/2011'	,	@EventTypeId = 125    , @Comment = 'MoreLabour'

