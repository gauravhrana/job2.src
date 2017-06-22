/******************************************************************************
**		Name: Needs
*******************************************************************************/

EXEC dbo.Needs_Insert @NeedsId = -27	,	@StudentId = 10,	@RequestDate = '1/1/2012',	@ReceivedDate = '3/2/2012', @NeedItemId = 1,	@NeedItemStatus = 'Good',   @NeedItemBy = 'Son'
EXEC dbo.Needs_Insert @NeedsId = -28	,	@StudentId = 11,	@RequestDate = '1/2/2012',	@ReceivedDate = '5/5/2012', @NeedItemId = 11,	@NeedItemStatus = 'Bad',	@NeedItemBy = 'Daughter'
EXEC dbo.Needs_Insert @NeedsId = -29	,	@StudentId = 12,	@RequestDate = '1/3/2012',	@ReceivedDate = '9/6/2012', @NeedItemId = 12,	@NeedItemStatus = 'Good',	@NeedItemBy = 'Mother'

