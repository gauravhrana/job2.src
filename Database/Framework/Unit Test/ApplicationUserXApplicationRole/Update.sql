/******************************************************************************
**		Name: ApplicationUserXApplicationRole
*******************************************************************************/

EXEC dbo.ApplicationUserXApplicationRoleUpdate @ApplicationUserXApplicationRoleId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationUserXApplicationRoleUpdate @ApplicationUserXApplicationRoleId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationUserXApplicationRoleUpdate @ApplicationUserXApplicationRoleId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

