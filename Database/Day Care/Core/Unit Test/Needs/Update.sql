/******************************************************************************
**		Name: Needs
*******************************************************************************/

EXEC dbo.Needs_Update @NeedsId = -127	,	@StudentId = 110,	@RequestDate = '1/3/2012',	@ReceivedDate = '3/12/2012', @NeedItemId = 11,	@NeedItemStatus = 'Good',   @NeedItemBy = 'Son'
EXEC dbo.Needs_Update @NeedsId = -128	,	@StudentId = 111,	@RequestDate = '1/2/2012',	@ReceivedDate = '5/15/2012', @NeedItemId = 111,	@NeedItemStatus = 'Bad',	@NeedItemBy = 'Daughter'
EXEC dbo.Needs_Update @NeedsId = -129	,	@StudentId = 112,	@RequestDate = '1/13/2012',	@ReceivedDate = '9/16/2012', @NeedItemId = 112,	@NeedItemStatus = 'Good',	@NeedItemBy = 'Mother'

