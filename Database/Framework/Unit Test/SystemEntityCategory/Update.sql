/******************************************************************************
**		Name: SystemEntityCategory
*******************************************************************************/

EXEC dbo.SystemEntityCategoryUpdate @SystemEntityCategoryId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.SystemEntityCategoryUpdate @SystemEntityCategoryId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.SystemEntityCategoryUpdate @SystemEntityCategoryId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

