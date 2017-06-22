/******************************************************************************
**		Name: UserPreference
*******************************************************************************/

EXEC dbo.UserPreferenceUpdate @UserPreferenceId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.UserPreferenceUpdate @UserPreferenceId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.UserPreferenceUpdate @UserPreferenceId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

