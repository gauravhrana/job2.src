/******************************************************************************
**		Name: UserPreferenceCategory
*******************************************************************************/

EXEC dbo.UserPreferenceCategoryUpdate @UserPreferenceCategoryId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.UserPreferenceCategoryUpdate @UserPreferenceCategoryId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.UserPreferenceCategoryUpdate @UserPreferenceCategoryId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

