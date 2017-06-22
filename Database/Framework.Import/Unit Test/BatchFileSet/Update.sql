/******************************************************************************
**		Name: BatchFileSet
*******************************************************************************/

EXEC dbo.BatchFileSetUpdate @BatchFileSetId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.BatchFileSetUpdate @BatchFileSetId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.BatchFileSetUpdate @BatchFileSetId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

