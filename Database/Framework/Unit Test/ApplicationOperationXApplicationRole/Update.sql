/******************************************************************************
**		Name: ApplicationOperationXApplicationRole
*******************************************************************************/

EXEC dbo.ApplicationOperationXApplicationRoleUpdate @ApplicationOperationXApplicationRoleId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationOperationXApplicationRoleUpdate @ApplicationOperationXApplicationRoleId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationOperationXApplicationRoleUpdate @ApplicationOperationXApplicationRoleId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

