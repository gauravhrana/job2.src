/******************************************************************************
**		Name: SystemEntityType
*******************************************************************************/

EXEC dbo.SystemEntityTypeUpdate @SystemEntityTypeId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.SystemEntityTypeUpdate @SystemEntityTypeId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.SystemEntityTypeUpdate @SystemEntityTypeId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

