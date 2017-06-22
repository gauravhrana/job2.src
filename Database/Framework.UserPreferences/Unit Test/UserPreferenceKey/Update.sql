/******************************************************************************
**		Name: UserPreferenceKey
*******************************************************************************/

EXEC dbo.UserPreferenceKeyUpdate @UserPreferenceKeyId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.UserPreferenceKeyUpdate @UserPreferenceKeyId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.UserPreferenceKeyUpdate @UserPreferenceKeyId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

