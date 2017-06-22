/******************************************************************************
**		Name: ApplicationEntityParentalHierarchy
*******************************************************************************/

EXEC dbo.ApplicationEntityParentalHierarchyUpdate @ApplicationEntityParentalHierarchyId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationEntityParentalHierarchyUpdate @ApplicationEntityParentalHierarchyId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationEntityParentalHierarchyUpdate @ApplicationEntityParentalHierarchyId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

