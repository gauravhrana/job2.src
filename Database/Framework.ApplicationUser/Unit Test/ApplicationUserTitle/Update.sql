/******************************************************************************
**		Name: ApplicationUserTitle
*******************************************************************************/

EXEC dbo.ApplicationUserTitleUpdate @ApplicationUserTitleId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationUserTitleUpdate @ApplicationUserTitleId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationUserTitleUpdate @ApplicationUserTitleId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

