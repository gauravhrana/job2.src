/******************************************************************************
**		Name: ApplicationOperation
*******************************************************************************/

EXEC dbo.ApplicationOperationUpdate @ApplicationOperationId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationOperationUpdate @ApplicationOperationId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationOperationUpdate @ApplicationOperationId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

