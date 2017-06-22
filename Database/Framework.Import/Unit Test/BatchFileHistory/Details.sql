/******************************************************************************
**		Name: BatchFileHistory
*******************************************************************************/

EXEC dbo.BatchFileHistoryUpdate @BatchFileHistoryId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.BatchFileHistoryUpdate @BatchFileHistoryId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.BatchFileHistoryUpdate @BatchFileHistoryId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

