/******************************************************************************
**		Name: ApplicationUser
*******************************************************************************/

EXEC dbo.ApplicationUserUpdate @ApplicationUserId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationUserUpdate @ApplicationUserId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationUserUpdate @ApplicationUserId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

