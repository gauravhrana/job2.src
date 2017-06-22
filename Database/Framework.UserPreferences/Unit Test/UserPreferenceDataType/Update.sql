/******************************************************************************
**		Name: UserPreferenceDataType
*******************************************************************************/

EXEC dbo.UserPreferenceDataTypeUpdate @UserPreferenceDataTypeId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.UserPreferenceDataTypeUpdate @UserPreferenceDataTypeId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.UserPreferenceDataTypeUpdate @UserPreferenceDataTypeId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

